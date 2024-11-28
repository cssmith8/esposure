using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeCard : MonoBehaviour
{
    private Vector3 originalSize = new Vector3(0.2f, 0.2f, 0.2f);  // Original scale of the object
    private Vector3 bigSize = new Vector3(0.4f, 0.4f, 0.4f);  // Scale when hovered
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public static ChallengeCard instance;
    [SerializeField] private GameObject temp;

    private void Start()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // This is called when the mouse enters the collider
    private void OnMouseEnter()
    {
        StopCoroutine(Shrink());  // Stop the shrink coroutine if running
        StartCoroutine(Grow());  // Start the grow coroutine
    }

    // This is called when the mouse exits the collider
    private void OnMouseExit()
    {
        StopCoroutine(Grow());  // Stop the grow coroutine if running
        StartCoroutine(Shrink());  // Start the shrink coroutine
    }



    private IEnumerator Grow()
    {
        //over half a second, grow the object to the big size
        float elapsedTime = 0;
        while (elapsedTime < 0.25f)
        {
            transform.localScale = Vector3.Lerp(originalSize, bigSize, (elapsedTime / 0.25f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = bigSize;
    }

    private IEnumerator Shrink()
    {
        //over half a second, shrink the object to the original size
        float elapsedTime = 0;
        while (elapsedTime < 0.25f)
        {
            transform.localScale = Vector3.Lerp(bigSize, originalSize, (elapsedTime / 0.25f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalSize;
    }
}
