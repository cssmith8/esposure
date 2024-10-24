using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    public void Submit()
    {
        //in default namespace
        hand.GetComponent<HandManager>().hideCards();
    }
}
