using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum CardState
{
    Idle = 0,
    Hovered = 1,
    Selected = 2,
    Hidden = 3,
}

public class Card : MonoBehaviour
{
    public string category;
    [SerializeField] private GameObject display;
    [SerializeReference] private GameObject slot;
    [SerializeReference] protected GameObject objRaised, objHovered, objIdle, objHidden;
    private bool isFlipped = false;
    public CardState state = CardState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        slot = transform.parent.parent.gameObject;
        objRaised = slot.transform.GetChild(0).GetChild(0).gameObject;
        objHovered = slot.transform.GetChild(0).GetChild(1).gameObject;
        objIdle = slot.transform.GetChild(0).GetChild(2).gameObject;
        objHidden = slot.transform.GetChild(0).GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Move(CardState target)
    {
        switch (target)
        {
            case CardState.Idle:
                StopCoroutine("MoveToTarget");
                StartCoroutine(MoveToTarget(objIdle.transform.localPosition));
                state = CardState.Idle;
                break;
            case CardState.Hovered:
                StopCoroutine("MoveToTarget");
                StartCoroutine(MoveToTarget(objHovered.transform.localPosition));
                state = CardState.Hovered;
                break;
            case CardState.Selected:
                StopCoroutine("MoveToTarget");
                StartCoroutine(MoveToTarget(objRaised.transform.localPosition));
                state = CardState.Selected;
                break;
            case CardState.Hidden:
                StopCoroutine("MoveToTarget");
                StartCoroutine(MoveToTarget(objHidden.transform.localPosition));
                state = CardState.Hidden;
                break;
        }
        
    }

    //coroutine to move the card up
    protected IEnumerator MoveToTarget(Vector3 target)
    {
        float time = Time.timeSinceLevelLoad;
        while (Vector3.Distance(transform.localPosition, target) > 0.1f)
        {
            while (Time.timeSinceLevelLoad - time < Time.fixedDeltaTime) yield return null; //lock this to 60fps
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, 0.08f);
            yield return null;
        }
        transform.localPosition = target;
    }

    public void Deselect()
    {
        Move(CardState.Idle);
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
