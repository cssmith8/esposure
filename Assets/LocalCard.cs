using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using CS = CardState;

public class LocalCard : Card
{

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        if (state == CS.Idle)
        {
            Move(CS.Hovered);
        }
    }

    void OnMouseExit()
    {
        if (state == CS.Hovered)
        {
            Move(CS.Idle);
        }
    }

    void OnMouseDown()
    {
        if (state == CS.Hovered)
        {
            LocalHand.instance.SelectCard(slot.GetComponent<CardSlot>().index);
            Flip();
            Select();
            return;
        }
        if (state == CS.Selected)
        {
            Deselect();
            return;
        }
    }
}
