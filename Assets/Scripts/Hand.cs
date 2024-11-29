using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Hand : MonoBehaviour {
    [SerializeField] protected float cardZGapSize = 0.1f;
    [SerializeField] protected float handWidth = 10f;

    // Prefabs
    public GameObject CardSlotPrefab;
    public GameObject CardPrefab;
    
    // Self
    protected int selectedCardIndex = -1;
    protected Vector3 anchorPos;
    protected Branch currentBranch = (Branch)1;
    protected bool isRevealOver = false;

    // References
    protected CardDataManager _cdm;
    protected List<CardSlot> slotList = new();
    protected List<int> slotOrder = new();
    protected List<Card> cardList = new();
    protected List<DisplayManager> displayManagerList = new();

    public virtual void Awake() {
        
    }
    
    private void Start() {
        anchorPos = transform.position;
        _cdm = CardDataManager.Instance;
    }

    public virtual void OnGameStart() {
    }

    protected void MakeHand(Branch b) {
        _cdm = CardDataManager.Instance;
        Debug.Log($"_cdm: {_cdm}, CardSlotPrefab: {CardSlotPrefab}, CardPrefab: {CardPrefab}");
        var cardBranch = _cdm.Roles[(int)b];
        var gapSize = handWidth / (cardBranch.Count - 1f);
        var index = 0;
        foreach (var cardInfo in cardBranch) {
            var pos = anchorPos + Vector3.right * (gapSize * index); // Set horizontal pos starting at 0
            pos += Vector3.left * handWidth / 2; // Move left so hand center is at 0
            var slotObj = Instantiate(CardSlotPrefab, pos, Quaternion.identity, transform);
            var slot = slotObj.GetComponent<CardSlot>();
            slot.index = index;
            slotOrder.Add(index);

            var cardObj = Instantiate(CardPrefab, slotObj.transform);
            var card = cardObj.GetComponent<Card>();
            // Debug.Log(cardInfo.ID);
            // Debug.Log(card);
            card.SetRoleID(cardInfo.ID);
            card.Assign(slotObj);
            var dm = card.DM;
            dm.startActive = isRevealOver;
            dm.setRole(cardInfo);

            slotList.Add(slot);
            cardList.Add(card);
            displayManagerList.Add(dm);
            index++;
        }

        MoveCardToTop(0);
    }

    public void MoveCardToTop(int cardIndex) {
        slotOrder[cardIndex] = 0;
        var highestNum = 0;

        for (int i = cardIndex - 1, distance = 1; i >= 0; i--, distance++) {
            slotOrder[i] = distance;
            if (distance > highestNum) highestNum = distance;
        }

        for (int i = cardIndex + 1, distance = 1; i < slotOrder.Count; i++, distance++) {
            slotOrder[i] = distance;
            if (distance > highestNum) highestNum = distance;
        }

        RepositionCards(highestNum);
    }

    // highestNum is the highest number in slotOrder
    protected void RepositionCards(int highestNum) {
        var index = 0;
        foreach (var zOrder in slotOrder) {
            var slot = slotList[index];
            var slotTransform = slot.transform;
            var currentPosition = slotTransform.position;
            currentPosition.z = anchorPos.z - highestNum * cardZGapSize + zOrder * cardZGapSize;
            slotTransform.position = currentPosition;
            index++;
        }
    }

    protected void RepositionCards() {
        RepositionCards(slotOrder.Max());
    }

    public void RevealAll() {
        StartCoroutine(RevealAllCards());
    }

    //coroutine to flip all cards
    protected IEnumerator RevealAllCards() {
        var totalTime = 0.5f;
        foreach (var card in cardList) {
            yield return new WaitForSeconds(totalTime / slotList.Count);
            card.FlipReveal();
        }
    }

    public GameObject GetSelectedCard() {
        if (selectedCardIndex < 0) return cardList[0].gameObject;

        Debug.Log($"Returning card at index {Mathf.Min(selectedCardIndex, cardList.Count - 1)}");
        return cardList[Mathf.Min(selectedCardIndex, cardList.Count - 1)].gameObject;
    }

    public void HideUnselected() {
        //for every slot
        for (var i = 0; i < slotList.Count; i++)
            //if the slot is the selected card
            if (i != selectedCardIndex)
                //hide the card
                if (slotList[i].card != null)
                    slotList[i].card.GetComponent<Card>().Hide();
    }

    public void SelectCard(int index) {
        for (var i = 0; i < cardList.Count; i++) {
            if (i != index) {
                cardList[i].Deselect();
            }
        }
        
        selectedCardIndex = index;
        GameManager.localInstance.SelectCard(selectedCardIndex);
    }
    
    public void DeselectCard() {
        selectedCardIndex = -1;
    }
}