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
    private ChallengeCard _challengeCard;

    void Start() {
        instance = this;
        Invoke(nameof(DealCards), 0.5f);
    }
    
    public void DealCards()
    {
        localHand = LocalHand.instance;
        enemyHand = EnemyHand.instance;
        _challengeCard = ChallengeCard.instance;
        
        localHand.OnGameStart();
        enemyHand.OnGameStart();
        _challengeCard.SetToRandomChallenge();
    }
}
