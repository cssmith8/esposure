using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalHand : MonoBehaviour {
    // References
    private List<CardSlot> slotList = new();
    private List<LocalCard> cardList = new();
    private List<DisplayManager> displayManagerList = new();
    private CardDataManager _cdm;
    
    // Self
    public static LocalHand instance;
    private int selectedCardIndex = -1;
    private Branch currentBranch = (Branch) 1;
    [SerializeField] private float cardZGapSize = 0.1f;
    [SerializeField] private float handWidth = 10f;
    private Vector3 anchorPos;
    private List<int> slotOrder = new();
    private Boolean isRevealOver = false;

    // Prefabs
    public GameObject CardSlotPrefab;
    public GameObject CardPrefab;
    
    void Awake() {
        _cdm = CardDataManager.Instance;
        anchorPos = transform.position;
    }

    void Start() {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple CardDataManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;

        SetHand(currentBranch);
        Invoke(nameof(RevealAll), 1.0f);
    }
    
    private void SetHand(Branch b) {
        if (slotList.Count != 0) {
            DestroyHand();
            isRevealOver = true;
        }
        MakeHand(b);
    }

    private void MakeHand(Branch b) {
        List<Role> cardBranch = _cdm.Roles[(int)b];
        float gapSize = handWidth / (cardBranch.Count - 1f);
        int index = 0;
        foreach (Role cardInfo in cardBranch) {
            Vector3 pos = anchorPos + Vector3.right * gapSize * index; // Set horizontal pos starting at 0
            pos += Vector3.left * handWidth/2; // Move left so hand center is at 0
            GameObject slotObj = Instantiate(CardSlotPrefab, pos, Quaternion.identity, transform);
            CardSlot slot = slotObj.GetComponent<CardSlot>();
            slot.index = index;
            slotOrder.Add(index);
            
            var cardObj = Instantiate(CardPrefab, slotObj.transform);
            var card = cardObj.GetComponent<LocalCard>();
            card.Assign(slotObj);
            var dm = card.PortraitManager;
            dm.startActive = isRevealOver;
            dm.setRole(cardInfo);

            slotList.Add(slot);
            cardList.Add(card);
            displayManagerList.Add(dm);
            index++;
        }
        
        MoveCardToTop(0);
    }

    private void DestroyHand() {
        foreach (var slot in slotList) Destroy(slot.gameObject);
        slotList.Clear();
        slotOrder.Clear();
        cardList.Clear();
        displayManagerList.Clear();
    }

    public void incrementBranch() {
        Branch branchToSet = (Branch)((int)currentBranch % 5 + 1);
        currentBranch = branchToSet;
        SetHand(currentBranch);
    }

    public void decrementBranch() {
        Branch branchToSet = (Branch)(((int)currentBranch + 3) % 5 + 1);
        currentBranch = branchToSet;
        SetHand(currentBranch);
    }
    
    public void MoveCardToTop(int cardIndex) {
        slotOrder[cardIndex] = 0;
        int highestNum = 0;
        
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
    private void RepositionCards(int highestNum) {
        int index = 0;
        foreach (int zOrder in slotOrder) {
            CardSlot slot = slotList[index];
            Transform slotTransform = slot.transform;
            Vector3 currentPosition = slotTransform.position;
            currentPosition.z = anchorPos.z - highestNum * cardZGapSize + zOrder * cardZGapSize;
            slotTransform.position = currentPosition;
            index++;
        }
    }
    
    private void RepositionCards() {
        RepositionCards(slotOrder.Max());
    }

    public void RevealAll() {
        StartCoroutine(RevealAllCards());
    }

    //coroutine to flip all cards
    private IEnumerator RevealAllCards() {
        float totalTime = 0.5f;
        foreach (var card in cardList) {
            yield return new WaitForSeconds(totalTime / slotList.Count);
            card.FlipReveal();
        }
    }

    public GameObject GetSelectedCard() {
        if (selectedCardIndex == -1) {
            return slotList[0].card;
        }

        return slotList[selectedCardIndex].card;
    }

    public void HideUnselected() {
        //for every slot
        for (int i = 0; i < slotList.Count; i++) {
            //if the slot is the selected card
            if (i != selectedCardIndex) {
                //hide the card
                if (slotList[i].card != null) {
                    slotList[i].card.GetComponent<Card>().Hide();
                }
            }
        }
    }

    public void SelectCard(int index) {
        for (int i = 0; i < slotList.Count; i++) {
            if (i != index) {
                if (slotList[i].card != null) {
                    slotList[i].card.GetComponent<Card>().Deselect();
                }
            }
        }

        selectedCardIndex = index;
        GameManager.localInstance.SelectCard(index);
    }

    public void DeselectCard() {
        selectedCardIndex = -1;
    }
}