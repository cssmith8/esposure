using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : Hand
{
    public static EnemyHand instance;
    private List<EnemyCard> cardList = new();
    
    public override void Awake() {
        Debug.Log("Enemy hand created");
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple hand instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;

        // for (int i = 0; i < 8; i++)
        // {
        //     GameObject slotObj = transform.GetChild(i).gameObject;
        //     slotList.Add(slotObj.GetComponent<CardSlot>());
        // }
    }
    
    public override void OnGameStart() {
        MakeHand((Branch)1);
        // Debug.Log("Enemy hand game start");
        // for (int i = 0; i < 8; i++)
        // {
        //     GameObject slotObj = transform.GetChild(i).gameObject;
        //     var cardObj = Instantiate(CardPrefab, slotObj.transform);
        //     //log the cards position
        //     Debug.Log("Card position: " + cardObj.transform.position);
        //     slotObj.GetComponent<CardSlot>().AssignCard(cardObj);
        //     cardObj.GetComponent<EnemyCard>().Assign(slotObj);
        //
        // }
    }

    // public new void SelectCard(int index)
    // {
    //     int displayIndex = index % 8;
    //
    //     for (var i = 0; i < slotList.Count; i++)
    //         if (i != displayIndex)
    //             slotList[i].card.GetComponent<EnemyCard>().Deselect();
    //
    //     selectedCardIndex = displayIndex;
    //     slotList[displayIndex].card.GetComponent<EnemyCard>().Select();
    // }

    public void TransformCards(int localid)
    {
        foreach (var card in cardList) {
            card.SetCardRoleByID(localid);
        }
    }
}
