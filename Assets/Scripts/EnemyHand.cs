using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    private List<GameObject> slots = new List<GameObject>();
    [HideInInspector] public static EnemyHand instance;
    private int selectedCardIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //for each child
        for (int i = 0; i < transform.childCount; i++)
        {
            //add the child to the slots array
            slots.Add(transform.GetChild(i).gameObject);
        }
    }

    public bool AssignToFreeSlot(GameObject card)
    {
        //for every slot
        for (int i = 0; i < slots.Count; i++)
        {
            //if the slot is empty
            if (slots[i].GetComponent<CardSlot>().card == null)
            {
                //assign the card to the slot
                card.GetComponent<Card>().Assign(slots[i]);
                return true;
            }
        }
        return false;
    }

    public GameObject GetSelectedCard()
    {
        if (selectedCardIndex == -1)
        {
            return slots[0].GetComponent<CardSlot>().card;
        }
        return slots[selectedCardIndex].GetComponent<CardSlot>().card;
    }

    public void HideUnselected()
    {
        //for every slot
        for (int i = 0; i < slots.Count; i++)
        {
            //if the slot is the selected card
            if (i != selectedCardIndex)
            {
                //hide the card
                if (slots[i].GetComponent<CardSlot>().card != null)
                {
                    slots[i].GetComponent<CardSlot>().card.GetComponent<Card>().Hide();
                }
            }
        }
    }

    public void SelectCard(int index)
    {
        selectedCardIndex = index;
        //for each slot
        for (int i = 0; i < slots.Count; i++)
        {
            //if the slot is the selected card
            if (i == index)
            {
                //select the card
                if (slots[i].GetComponent<CardSlot>().card != null)
                {
                    slots[i].GetComponent<CardSlot>().card.GetComponent<Card>().Select();
                }
                
            } else
            {
                //deselect the card
                if (slots[i].GetComponent<CardSlot>().card != null)
                {
                    slots[i].GetComponent<CardSlot>().card.GetComponent<Card>().Deselect();
                }
            }
        }
    }
}
