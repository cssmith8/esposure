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

    // Update is called once per frame
    void Update()
    {

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
                slots[i].transform.GetChild(1).GetChild(0).GetComponent<Card>().Hide();
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
                slots[i].transform.GetChild(1).GetChild(0).GetComponent<Card>().Select();
            } else
            {
                //deselect the card
                slots[i].transform.GetChild(1).GetChild(0).GetComponent<Card>().Deselect();
            }
        }
    }
}
