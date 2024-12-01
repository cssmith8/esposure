using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalHand : Hand {
    public static LocalHand instance;
    [SerializeField] private TextMeshProUGUI BranchText;
    
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

        SetBranchText();
    }
    
    public override void OnGameStart() {
        SetHand(currentBranch, false);
        Invoke(nameof(RevealAll), 1.0f);
    }

    public int GetSelectedCardIndex()
    {
        return selectedCardIndex;
    }
    
    private void SetHand(Branch b, bool showCards) {
        if (slotList.Count != 0) {
            DestroyHand();
            // isRevealOver = true;
        }
        MakeHand(b, showCards);
    }

    public void DestroyHand() {
        foreach (var slot in slotList) Destroy(slot.gameObject);
        slotList.Clear();
        slotOrder.Clear();
        cardList.Clear();
        displayManagerList.Clear();
    }

    public void incrementBranch() {
        Branch branchToSet = (Branch)((int)currentBranch % 5 + 1);
        currentBranch = branchToSet;
        SetBranchText();
        SetHand(currentBranch, true);
    }

    public void decrementBranch() {
        Branch branchToSet = (Branch)(((int)currentBranch + 3) % 5 + 1);
        currentBranch = branchToSet;
        SetBranchText();
        SetHand(currentBranch, true);
    }

    private void SetBranchText() {
        BranchText.text = (Enum.GetName(typeof(Branch), currentBranch) + " Branch").ToUpper();
    }
}