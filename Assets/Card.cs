using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string category;
    [SerializeField] private GameObject display;
    private bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipReveal()
    {
        if (isFlipped)
        {
            StartCoroutine(FlipR());
            isFlipped = false;
        }
    }

    //coroutine to flip the card
    public IEnumerator FlipR()
    {
        float time = 0;
        float fliptime = 0.5f;
        while (time < fliptime) {
            display.transform.localScale = new Vector3(1 - time / fliptime, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        display.transform.localScale = new Vector3(0, 1, 1);
        foreach (Transform child in display.transform)
        {
            child.gameObject.SetActive(true);
        }
        time = 0;
        while (time < fliptime)
        {
            display.transform.localScale = new Vector3(time / fliptime, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        display.transform.localScale = new Vector3(1, 1, 1);
    }

    public void FlipHide()
    {
        if (!isFlipped)
        {
            StartCoroutine(FlipH());
            isFlipped = true;
        }
    }

    //coroutine to flip the card
    public IEnumerator FlipH()
    {
        float time = 0;
        float fliptime = 0.5f;
        while (time < fliptime)
        {
            display.transform.localScale = new Vector3(1 - time / fliptime, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        display.transform.localScale = new Vector3(0, 1, 1);
        foreach (Transform child in display.transform)
        {
            child.gameObject.SetActive(false);
        }
        time = 0;
        while (time < fliptime)
        {
            display.transform.localScale = new Vector3(time / fliptime, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        display.transform.localScale = new Vector3(1, 1, 1);
    }
}
