using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHand : MonoBehaviour
{
    private GameObject[] slots;
    [HideInInspector] public static LocalHand instance;
    private int selectedCardIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //for each child
        for (int i = 0; i < transform.childCount; i++)
        {
            //add the child to the slots array
            slots[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSelectedCard()
    {
        return selectedCardIndex;
    }

    public void SelectCard(GameObject card)
    {
        //for every slot
        for (int i = 0; i < slots.Length; i++)
        {
            //if the slot is the card
            if (slots[i].transform.GetChild(1).GetChild(0) == card)
            {
                //set the selected card index to the slot index
                selectedCardIndex = i;
            } else
            {
                slots[i].transform.GetChild(1).GetChild(0).GetComponent<Card>().Deselect();
            }
        }
    }

    public void DeselectCard()
    {
        selectedCardIndex = -1;
    }
}
