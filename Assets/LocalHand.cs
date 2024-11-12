using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHand : MonoBehaviour
{
    private List<GameObject> slots = new List<GameObject>();
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
            slots.Add(transform.GetChild(i).gameObject);
        }
        StartCoroutine(RevealAll());
    }

    //coroutine to flip all cards
    public IEnumerator RevealAll()
    {
        float totalTime = 0.5f;
        for (int i = 0; i < slots.Count; i++)
        {
            yield return new WaitForSeconds(totalTime / slots.Count);
            slots[i].GetComponent<CardSlot>().RevealCard();
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

    public void HideUnselected()
    {
        Debug.Log(selectedCardIndex);
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
        for (int i = 0; i < slots.Count; i++)
        {
            if (i != index)
            {
                slots[i].transform.GetChild(1).GetChild(0).GetComponent<Card>().Deselect();
            }
        }
        selectedCardIndex = index;
    }

    public void DeselectCard()
    {
        selectedCardIndex = -1;
    }
}
