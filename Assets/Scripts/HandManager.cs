using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour {
    private List<GameObject> cardList = new List<GameObject>();
    private List<DisplayManager> dmList = new List<DisplayManager>();
    private List<CardSlot> csList = new List<CardSlot>();
    public Branch cardFamily;
    
    private void Start() {
        // todo replace grab children method with something more specific
        int slotID = 0;
        for (int i = 0; i < transform.childCount; i++) {
            var card = transform.GetChild(i).GetChild(1);
            var display = card.GetChild(0);
            var childSR = display.GetComponent<DisplayManager>();
            var cardSlot = card.GetComponent<CardSlot>();
            if (childSR) {
                dmList.Add(childSR);
                csList.Add(cardSlot);
                cardList.Add(card.GameObject());
                cardSlot.SetSlotID(slotID);
                cardSlot.hm = this;
                slotID++;
            }
        }
    }
    
    // public void lowerOtherCards()

    public void setImages(Branch branch) {
        cardFamily = branch;
        foreach (DisplayManager dm in dmList)
        {
            dm.setImage(branch);
        }
    }

    public void selectCard(int cardID) {
        foreach (CardSlot slot in csList) {
            if (slot.id == cardID) {
                slot.Select();
                continue;
            }
            slot.Deselect();
        }
    }

    public void incrementBranch() {
        Branch branchToSet = (Branch)(((int)cardFamily % 5) + 1); // todo ew lol
        cardFamily = branchToSet;
        setImages(branchToSet);
    }
    
    public void decrementBranch() {
        Branch branchToSet = (Branch)((((int)cardFamily + 3) % 5) + 1); // todo ew lol
        cardFamily = branchToSet;
        setImages(branchToSet);
    }
}