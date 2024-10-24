using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CS = CardState;

public class LocalCardSlot : CardSlot
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
            hm.selectCard(id);
            return;
        }
        if (state == CS.Selected)
        {
            Deselect();
            GameManager.localInstance.SelectRole(-1);
            return;
        }
    }

    public void Hide()
    {
        state = CS.Hidden;
        target = hidden;
        Move();
    }
}
