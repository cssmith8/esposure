using System.Collections;
using UnityEngine;

public class ReviewArea : MonoBehaviour {
    [HideInInspector] public static ReviewArea instance;
    [SerializeField] private GameObject localSlot, enemySlot, correctSpawn, correctTarget;
    [SerializeField] public GameObject enemyPlusOne, enemyPlusTwo, localPlusOne, localPlusTwo;
    private CardDataManager _cdm;
    private ChallengeCard challengeCard;
    [HideInInspector] private GameObject localCardObj, enemyCardObj;
    [SerializeField] private GameObject correctCard;

    private void Start() {
        instance = this;
        challengeCard = ChallengeCard.instance;
        _cdm = CardDataManager.Instance;
    }

    public void ReviewSequence() {
        StartCoroutine(Review());
    }

    //coroutine
    private IEnumerator Review() {
        yield return new WaitForSeconds(0.5f);
        GrabLocalCard();
        GrabEnemyCard();
        yield return new WaitForSeconds(1f);
        enemyCardObj.GetComponent<Card>().FlipReveal();
        yield return new WaitForSeconds(1f);

        //reveal correct card
        string role = ChallengeCard.instance.currentChallenge.Role;
        GameObject go = Instantiate(correctCard, correctSpawn.transform.position, Quaternion.identity);
        go.GetComponent<EnemyCard>().SetCardRoleByID(_cdm.GetRoleID(role));
        Debug.Log($"Correct card role: {role}");

        //move correct card to target over 1/2 second
        float time = 0;
        while (time < 0.5f)
        {
            go.transform.position = Vector3.Lerp(correctSpawn.transform.position, correctTarget.transform.position, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        go.transform.position = correctTarget.transform.position;

        yield return new WaitForSeconds(0.5f);

        go.GetComponent<Card>().FlipReveal();

        yield return new WaitForSeconds(1.5f);

        
        var currentChallenge = challengeCard.currentChallenge;
        
        var localCard = localCardObj.GetComponent<Card>();
        var localRole = _cdm.RoleDict[localCard.RoleID];
        var localScore = 0;

        var enemyCard = enemyCardObj.GetComponent<Card>();
        var enemyRole = _cdm.RoleDict[enemyCard.RoleID];
        var enemyScore = 0;
        
        Debug.Log($"Challenge - enum: {currentChallenge.BranchEnum}, name: {currentChallenge.Role}");
        Debug.Log($"Local Role - enum: {localRole.BranchEnum}, name: {localRole.Name}");
        Debug.Log($"Enemy Role - enum: {enemyRole.BranchEnum}, name: {enemyRole.Name}");
        
        // Check branch
        if (localRole.BranchEnum == currentChallenge.BranchEnum) {
            Debug.Log("Local branch is correct");
            localScore++;
        }
        else {
            Debug.Log("Local branch is wrong");
        }
        if (enemyRole.BranchEnum == currentChallenge.BranchEnum) {
            Debug.Log("Enemy branch is correct");
            enemyScore++;
        }
        else {
            Debug.Log("Enemy branch is wrong");
        }

        // Check role
        if (localRole.Name == currentChallenge.Role) {
            Debug.Log("Local role is correct");
            localScore++;
        }
        else {
            Debug.Log("Local role is wrong");
        }

        if (enemyRole.Name == currentChallenge.Role) {
            Debug.Log("Enemy role is correct");
            enemyScore++;
        }
        else {
            Debug.Log("Enemy role is wrong");
        }

        if (localScore == 2)
            Instantiate(localPlusTwo, localSlot.transform);
        else if (localScore == 1)
            Instantiate(localPlusOne, localSlot.transform);
        
        if (enemyScore == 2)
            Instantiate(enemyPlusTwo, enemySlot.transform);
        else if (enemyScore == 1)
            Instantiate(enemyPlusOne, enemySlot.transform);
        
        // disable temporarily
        // if (enemyScore > 0) GameManager.localInstance.AddEnemyScore(enemyScore);
        
        // this is necessary as it clears the reference lists for localHand's slots, cards, etc.
        LocalHand.instance.DestroyHand();

        Destroy(go);
        //destroy rest of cards
        var cards = GameObject.FindGameObjectsWithTag("Card");
        Debug.Log(cards.Length);
        foreach (var card in cards)
        {
            Card real = card.GetComponent<Card>();
            if (real) real.StartDestroy();
        }
        //assign new cards
        Invoke(nameof(Deal), 0.5f);
    }

    private void Deal() {
        Deck.instance.DealCards();
    }

    public void GrabLocalCard() {
        localCardObj = LocalHand.instance.GetSelectedCard();
        localCardObj.transform.SetParent(transform);
        localCardObj.GetComponent<Card>().MoveTo(localSlot.transform.localPosition);
    }

    public void GrabEnemyCard() {
        enemyCardObj = EnemyHand.instance.GetSelectedCard();
        enemyCardObj.transform.SetParent(transform);
        enemyCardObj.GetComponent<Card>().MoveTo(enemySlot.transform.localPosition);
    }

    public Vector3 GetEnemyPosition() {
        return enemySlot.transform.position;
    }
}