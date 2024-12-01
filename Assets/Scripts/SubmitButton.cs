using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    public void Submit()
    {
        if (LocalHand.instance.GetSelectedCardIndex() == -1)
        {
            return;
        }
        if (GameManager.localInstance.submitted)
        {
            return;
        }

        //in default namespace
        LocalHand.instance.HideUnselected();
        GameManager.localInstance.Submit();

        //get all gameobjects tagged "Card" and disable the box collider 2d on them
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            card.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
