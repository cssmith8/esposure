using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    private GameObject[] slots;
    [HideInInspector] public static EnemyHand instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //for each child
        for (int i = 0; i < transform.childCount; i++)
        {
            //add the child to the slots array
            slots[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
