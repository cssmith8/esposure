using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : Hand
{
    public static EnemyHand instance;
    
    public override void Awake() {
        Debug.Log("Enemy hand created");
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple hand instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    public override void OnGameStart() {
        MakeHand((Branch)1);
    }
}
