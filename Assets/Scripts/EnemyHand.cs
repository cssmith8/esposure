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
                transform.GetChild(i).GetChild(1).GetComponent<EnemyCardSlotSlot>().Move(true);
            }
            else
            {
                transform.GetChild(i).GetChild(1).GetComponent<EnemyCardSlotSlot>().Move(false);
            }
        }
    }

    public void HideCard(int index)
    {
        //Debug.Log("Hiding card at index " + index);
        int real = transform.childCount;
        if (index >= real) return;
        transform.GetChild(index).GetChild(1).GetComponent<EnemyCardSlotSlot>().Hide();
    }

    public void ResetCards()
    {
        int real = transform.childCount;
        for (int i = 0; i < real; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<EnemyCardSlotSlot>().Move(false);
        }
    }
}
