using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public static Deck instance;
    [SerializeField] private List<Sprite> tempChallenges = new();
    private LocalHand localHand;
    private EnemyHand enemyHand;

    void Start() {
        instance = this;
        Invoke(nameof(DealCards), 0.5f);
    }
    
    public void DealCards()
    {
        localHand = LocalHand.instance;
        enemyHand = EnemyHand.instance;
        ChallengeCard.instance.UpdateSprite(tempChallenges[Random.Range(0,tempChallenges.Count)]);
        localHand.OnGameStart();
        enemyHand.OnGameStart();
    }
}
