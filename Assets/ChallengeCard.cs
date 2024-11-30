using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChallengeCard : MonoBehaviour
{
    private Vector3 originalSize = new Vector3(0.2f, 0.2f, 0.2f);  // Original scale of the object
    private Vector3 bigSize = new Vector3(0.4f, 0.4f, 0.4f);  // Scale when hovered
    // private SpriteRenderer spriteRenderer;
    public float GrowShrinkTime = 0.1f;
    [HideInInspector] public static ChallengeCard instance;
    [SerializeField] private GameObject descriptionTexObj; 
    private TextMeshProUGUI descriptionText;
    private CardDataManager _cdm;
    public Challenge currentChallenge { get; private set; }

    private void Awake() {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple challenge card instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        // _cdm = CardDataManager.Instance;
    }

    private void Start() {
        descriptionText = descriptionTexObj.GetComponent<TextMeshProUGUI>();
        
        _cdm = CardDataManager.Instance;
    }

    public void UpdateCard(int newChallengeIndex) {
        currentChallenge = _cdm.ChallengesFlatList[newChallengeIndex];
        descriptionText.text = currentChallenge.Description.ToUpper();
        Debug.Log($"Challenge card answer is {currentChallenge.Role}");
    }
    
    public void SetToRandomChallenge() {
        int newChallengeIndex = Random.Range(0, _cdm.ChallengesFlatList.Count);
        UpdateCard(newChallengeIndex);
    }
    
    private void OnMouseEnter()
    {
        StopCoroutine(Shrink());  // Stop the shrink coroutine if running
        StartCoroutine(Grow());  // Start the grow coroutine
    }
    
    private void OnMouseExit()
    {
        StopCoroutine(Grow());  // Stop the grow coroutine if running
        StartCoroutine(Shrink());  // Start the shrink coroutine
    }

    private IEnumerator Grow()
    {
        //over half a second, grow the object to the big size
        float elapsedTime = 0;
        while (elapsedTime < GrowShrinkTime)
        {
            transform.localScale = Vector3.Lerp(originalSize, bigSize, (elapsedTime / GrowShrinkTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = bigSize;
    }

    private IEnumerator Shrink()
    {
        //over half a second, shrink the object to the original size
        float elapsedTime = 0;
        while (elapsedTime < GrowShrinkTime)
        {
            transform.localScale = Vector3.Lerp(bigSize, originalSize, (elapsedTime / GrowShrinkTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalSize;
    }
}
