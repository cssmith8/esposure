using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    public void Submit()
    {
        //in default namespace
        LocalHand.instance.HideUnselected();
        GameManager.localInstance.Submit();
    }
}
