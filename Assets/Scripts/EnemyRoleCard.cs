using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using CS = CardState;

public class EnemyRoleCard : RoleCard
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
}
