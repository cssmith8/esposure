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
