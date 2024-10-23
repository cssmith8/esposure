using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    public void SelectCard(int index)
    {
        int real = transform.childCount;
        for (int i = 0; i < real; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).GetChild(3).GetComponent<EnemyCardSlotSlot>().Move(true);
            }
            else
            {
                transform.GetChild(i).GetChild(3).GetComponent<EnemyCardSlotSlot>().Move(false);
            }
        }
    }
}
