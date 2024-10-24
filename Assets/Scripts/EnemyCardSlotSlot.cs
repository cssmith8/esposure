using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using CS = CardState;

public class EnemyCardSlotSlot : CardSlot
{
    protected override void Start() {
        base.Start();
        IsLocal = false;
    }
    
    public void Move(bool selected)
    {
        if (selected)
        {
            target = raised;
            state = CS.Selected;
        }
        else
        {
            target = idle;
            state = CS.Idle;
        }

        base.Move();
    }

    public void Hide()
    {
        target = hidden;
        state = CS.Hidden;
        base.Move();
    }
}
