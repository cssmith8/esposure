using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private GameObject objRaised, objHovered, objIdle, objHidden;
    [HideInInspector] private GameObject card = null;
    [SerializeField] public int index = 0;

    public GameObject RaisedPos() { return objRaised; }
    public GameObject HoveredPos() { return objHovered; }
    public GameObject IdlePos() { return objIdle; }
    public GameObject HiddenPos() { return objHidden; }

    public void AssignCard(GameObject card)
    {
        this.card = card;
    }

    public void ForgetCard()
    {
        card = null;
    }

    public void RevealCard()
    {
        if (card)
        {
            card.GetComponent<Card>().FlipReveal();
        }
    }

    public void HideCard()
    {
        if (card)
        {
            card.GetComponent<Card>().FlipHide();
        }
    }

}
