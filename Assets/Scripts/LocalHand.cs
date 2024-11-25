using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalHand : Hand {
    public static LocalHand instance;
    
    public override void Awake() {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple hand instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        anchorPos = transform.position;
        _cdm = CardDataManager.Instance;
    }
    
    public override void OnGameStart() {
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
}