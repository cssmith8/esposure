using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [HideInInspector] public static Deck instance;
    [SerializeField] private GameObject lcyber, ltreasurer, ecyber, etreasurer;
    [SerializeField] private List<Sprite> tempChallenges = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Invoke("DealCards", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // todo draw actual cards
    public void DealCards()
    {
        ChallengeCard.instance.UpdateSprite(tempChallenges[Random.Range(0,tempChallenges.Count)]);
        // for (int i = 0; i < 4; i++)
        // {
        //     GameObject lcyberCard = Instantiate(lcyber, transform);
        //     if (!LocalHand.instance.AssignToFreeSlot(lcyberCard))
        //     {
        //         Destroy(lcyberCard);
        //         Debug.Log("Error: No free slots");
        //     }
        // }
        // GameObject ltreasurerCard = Instantiate(ltreasurer, transform);
        // if (!LocalHand.instance.AssignToFreeSlot(ltreasurerCard))
        // {
        //     Destroy(ltreasurerCard);
        //     Debug.Log("Error: No free slots");
        // }

        for (int i = 0; i < 4; i++)
        {
            GameObject ecyberCard = Instantiate(ecyber, transform);
            if (!EnemyHand.instance.AssignToFreeSlot(ecyberCard))
            {
                Destroy(ecyberCard);
                Debug.Log("Error: No free slots");
            }
        }
        GameObject etreasurerCard = Instantiate(etreasurer, transform);
        if (!EnemyHand.instance.AssignToFreeSlot(etreasurerCard))
        {
            Destroy(etreasurerCard);
            Debug.Log("Error: No free slots");
        }
        // Invoke("LocalRevealAll", 1.0f);
    }

    // public void LocalRevealAll()
    // {
    //     LocalHand.instance.RevealAll();
    // }
}
