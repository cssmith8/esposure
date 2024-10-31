using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class HandManager : MonoBehaviour {
        //Singleton
        [HideInInspector] public static HandManager localInstance;

        private List<DisplayManager> dmList = new List<DisplayManager>();
        public Branch cardFamily;
        [HideInInspector] public int selectedCardIndex = 0;
        
        private void Start() {
            localInstance = this;
            // todo replace grab children method with something more specific
            for (int i = 0; i < transform.childCount; i++) {
                var childSR = transform.GetChild(i)
                    .GetChild(1)
                    .GetChild(0)
                    .GetComponent<DisplayManager>() ?? null;
                if (childSR) {
                    dmList.Add(childSR);
                }
            }
        }

        public void setImages(Branch branch) {
            cardFamily = branch;
            foreach (DisplayManager dm in dmList)
            {
                dm.setImage(branch);
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

        public void hideCards()
        {
            // for every child
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == selectedCardIndex) continue;
                transform.GetChild(i).GetChild(1).GetComponent<LocalCardSlotSlot>().Hide();
                GameManager.localInstance.HideRole(i);
            }
        }
    }
}