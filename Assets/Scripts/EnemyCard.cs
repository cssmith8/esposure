using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card {
    private CardDataManager CDM;
    public int CardIndex { get; private set; }
    
    public override void Awake() {
        DM = display.GetComponent<DisplayManager>();
        Hand = EnemyHand.instance;
        CDM = CardDataManager.Instance;
    }

    public void SetCardRoleByID(int roleID) {
        SetCardRole(CDM.RoleDict[roleID]);
        roleID = RoleID;
    }
    
    public void SetCardRole(Role role) {
        DM.setRole(role);
    }
    
    public override void FlipReveal()
    {
        if (isFlipped)
        {
            //SetCardRoleByID(RoleID);
            StartCoroutine(FlipR());
            isFlipped = false;
        }
    }
}
