using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHand : MonoBehaviour {
    private List<CardSlot> slotList = new();
    private List<LocalCard> cardList = new();
    private List<DisplayManager> displayManagerList = new();
    public static LocalHand instance;
    public GameObject CardSlotPrefab;
    public GameObject CardPrefab;
    private int selectedCardIndex = -1;
    private Branch currentBranch = (Branch)1;
    private CardDataManager _cdm;
    private Stack<LocalCard> cardStack;

    void Awake() {
        _cdm = CardDataManager.Instance;
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
        Invoke("RevealAll", 1.0f);
    }
    
    private void SetHand(Branch b) {
        DestroyHand();
        MakeHand(b);
    }

    private void MakeHand(Branch b) {
        List<Role> cardBranch = _cdm.Roles[(int)b];
        float gapSize = 10f / (cardBranch.Count - 1f);
        int index = 0;
        foreach (Role cardInfo in cardBranch) {
            Vector3 pos = transform.position + Vector3.right * gapSize * index + Vector3.left * 5f;
            GameObject slotObj = Instantiate(CardSlotPrefab, pos, Quaternion.identity, transform);
            CardSlot slot = slotObj.GetComponent<CardSlot>();
            slot.index = index;

            var cardObj = Instantiate(CardPrefab, slotObj.transform);
            var card = cardObj.GetComponent<LocalCard>();
            card.Assign(slotObj);
            var dm = card.PortraitManager;
            dm.setRole(cardInfo);

            slotList.Add(slot);
            cardList.Add(card);
            displayManagerList.Add(dm);
            index++;
        }
    }

    private void DestroyHand() {
        foreach (var slot in slotList) {
            Destroy(slot.gameObject);
        }
        slotList.Clear();
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

    public bool AssignToFreeSlot(GameObject card) {
        //for every slot
        for (int i = 0; i < slotList.Count; i++) {
            //if the slot is empty
            if (slotList[i].GetComponent<CardSlot>().card == null) {
                //assign the card to the slot
                card.GetComponent<Card>().Assign(slotList[i].gameObject);
                return true;
            }
        }

        return false;
    }

    public GameObject GetSelectedCard() {
        if (selectedCardIndex == -1) {
            return slotList[0].GetComponent<CardSlot>().card;
        }

        return slotList[selectedCardIndex].GetComponent<CardSlot>().card;
    }

    public void HideUnselected() {
        //for every slot
        for (int i = 0; i < slotList.Count; i++) {
            //if the slot is the selected card
            if (i != selectedCardIndex) {
                //hide the card
                if (slotList[i].GetComponent<CardSlot>().card != null) {
                    slotList[i].GetComponent<CardSlot>().card.GetComponent<Card>().Hide();
                }
            }
        }
    }

    public void SelectCard(int index) {
        for (int i = 0; i < slotList.Count; i++) {
            if (i != index) {
                if (slotList[i].GetComponent<CardSlot>().card != null) {
                    slotList[i].GetComponent<CardSlot>().card.GetComponent<Card>().Deselect();
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