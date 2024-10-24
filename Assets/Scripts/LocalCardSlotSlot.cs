using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CS = CardState;

public class LocalCardSlotSlot : CardSlot
{
    protected override void Start() {
        base.Start();
        IsLocal = true;
    }
    
    void OnMouseEnter()
    {
        if (state == CS.Idle || state == CS.Hovered)
        {
            state = CS.Hovered;
            target = hovered;
            Move();
        }
    }

    void OnMouseExit()
    {
        if (state == CS.Idle || state == CS.Hovered)
        {
            state = CS.Idle;
            target = idle;
            Move();
        }
    }
    
    void OnMouseDown()
    {
        if (state == CS.Hovered)
        {
            Select();
            return;
        }
        if (state == CS.Selected)
        {
            GameManager.localInstance.SelectRole(-1);
            Deselect();
            return;
        }
    }

    private void Select()
    {
        state = CS.Selected;
        target = raised;
        Move();
        GameManager.localInstance.SelectRole(slotID);
        GameObject localCards = transform.parent.parent.gameObject;
        localCards.GetComponent<HandManager>().selectedCardIndex = slotID;
        for (int i = 0; i < localCards.transform.childCount; i++)
        {
            if (i != slotID)
            {
                localCards.transform.GetChild(i).GetChild(1).GetComponent<LocalCardSlotSlot>().Deselect();
            }
        }
    }

    public void Deselect()
    {
        state = CS.Idle;
        target = idle;
        Move();
    }

    public void Hide()
    {
        if (state == CS.Idle || state == CS.Hovered)
        {
            state = CS.Hidden;
            target = hidden;
        } else
        {
            state = CS.Idle;
            target = idle;
        }
        
        Move();
    }
}
