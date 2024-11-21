using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHand : MonoBehaviour
{
    private List<CardSlot> slots = new List<CardSlot>();
    private List<DisplayManager> dmList = new List<DisplayManager>();
    public static LocalHand instance;
    public GameObject CardSlotPrefab;
    private int selectedCardIndex = -1;
    private Branch currentBranch = (Branch)1;
    private CardDataManager _cdm;

    void Awake() {
        _cdm = CardDataManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //for each child
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     //add the child to the slots array
        //     slots.Add(transform.GetChild(i).gameObject);
        // }
        
        
        MakeHand(currentBranch);
    }

    void MakeHand(Branch b) {
        List<Role> cardBranch = _cdm.Roles[(int)currentBranch];
        float gapSize = 10f / (cardBranch.Count - 1f);
        int index = 0; 
        foreach (var card in cardBranch) {
            Vector3 pos = transform.position + Vector3.right * gapSize * index + Vector3.left * 5f;
            var slotObj = Instantiate(CardSlotPrefab, pos, Quaternion.identity, transform);
            var slot = slotObj.GetComponent<CardSlot>();
            // var cardObj = slotObj.
            slots.Add(slot);

            index++;
        }
    }

    public void RevealAll()
    {
        StartCoroutine(RevealAllCards());
    }

    //coroutine to flip all cards
    private IEnumerator RevealAllCards()
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

    public bool AssignToFreeSlot(GameObject card)
    {
        //for every slot
        for (int i = 0; i < slots.Count; i++)
        {
            //if the slot is empty
            if (slots[i].GetComponent<CardSlot>().card == null)
            {
                //assign the card to the slot
                card.GetComponent<Card>().Assign(slots[i].gameObject);
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
        for (int i = 0; i < slots.Count; i++)
        {
            if (i != index)
            {
                if (slots[i].GetComponent<CardSlot>().card != null)
                {
                    slots[i].GetComponent<CardSlot>().card.GetComponent<Card>().Deselect();
                }
            }
        }
        selectedCardIndex = index;
        GameManager.localInstance.SelectCard(index);
    }

    public void DeselectCard()
    {
        selectedCardIndex = -1;
    }
}
