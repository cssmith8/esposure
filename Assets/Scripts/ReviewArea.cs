using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewArea : MonoBehaviour
{
    [HideInInspector] public static ReviewArea instance;
    [SerializeField] private GameObject localSlot, enemySlot;

    [HideInInspector] private GameObject localCard, enemyCard;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void ReviewSequence()
    {
        StartCoroutine(Review());
    }

    //coroutine
    private IEnumerator Review()
    {
        yield return new WaitForSeconds(0.5f);
        GrabLocalCard();
        GrabEnemyCard();
        yield return new WaitForSeconds(1f);
        enemyCard.GetComponent<Card>().FlipReveal();
        yield return new WaitForSeconds(1f);
        int localScore = localCard.GetComponent<Card>().branch == Branch.Finance ? 1 : 0;
        int enemyScore = enemyCard.GetComponent<Card>().branch == Branch.Finance ? 1 : 0;
        Scoreboard.instance.UpdateScore(Scoreboard.instance.GetLocalScore() + localScore, Scoreboard.instance.GetEnemyScore() + enemyScore);
        //destroy all cards
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        Debug.Log(cards.Length);
        foreach (GameObject card in cards)
        {
            card.GetComponent<Card>().StartDestroy();
        }
        //assign new cards
        Invoke(nameof(Deal), 0.5f);
    }   

    private void Deal()
    {
        Deck.instance.DealCards();
    }

    public void GrabLocalCard()
    {
        localCard = LocalHand.instance.GetSelectedCard();
        localCard.transform.SetParent(transform);
        localCard.GetComponent<Card>().MoveTo(localSlot.transform.localPosition);
    }

    public void GrabEnemyCard()
    {
        enemyCard = EnemyHand.instance.GetSelectedCard();
        enemyCard.transform.SetParent(transform);
        enemyCard.GetComponent<Card>().MoveTo(enemySlot.transform.localPosition);
    }

}
