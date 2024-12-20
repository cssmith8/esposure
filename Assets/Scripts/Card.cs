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

public abstract class Card : MonoBehaviour
{
    // References
    [SerializeField] protected GameObject display;
    public DisplayManager DM { get; protected set; }
    protected GameObject slot;
    protected GameObject objRaised, objHovered, objIdle, objHidden;
    protected Hand Hand;
    
    // Self
    protected bool isFlipped = true;
    public int HandIndex { get; protected set; } // horizontal index in the hand order
    public CardState state = CardState.Idle;
    [SerializeField] public Branch branch;
    public int RoleID { get; protected set; }
    

    public virtual void Awake() { 
        DM = display.GetComponent<DisplayManager>();
    }

    public void SetRoleID(int roleID) {
        RoleID = roleID;
    }

    public void Assign(GameObject slot)
    {
        this.slot = slot;
        CardSlot s = slot.GetComponent<CardSlot>();
        s.AssignCard(gameObject);
        HandIndex = s.index;
        objRaised = s.RaisedPos();
        objHovered = s.HoveredPos();
        objIdle = s.IdlePos();
        objHidden = s.HiddenPos();
        transform.localPosition = objHidden.transform.localPosition;
        Move(CardState.Idle);
    }

    public void MoveTo(Vector3 pos)
    {
        StopCoroutine("MoveToTarget");
        StartCoroutine(MoveToTarget(pos, 0.5f));
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
    
    protected IEnumerator MoveToTarget(Vector3 target, float t = 0.2f)
    {
        Vector3 initial = transform.localPosition;
        float time = Time.timeSinceLevelLoad;
        float totalTime = t;
        while (Time.timeSinceLevelLoad - time < totalTime)
        {
            float progress = (Time.timeSinceLevelLoad - time) / totalTime;
            transform.localPosition = new Vector3(
                Mathf.SmoothStep(initial.x, target.x, progress),
                Mathf.SmoothStep(initial.y, target.y, progress),
                Mathf.SmoothStep(initial.z, target.z, progress)
            );
            yield return null;
        }
        transform.localPosition = target;
    }

    public void Deselect()
    {
        Move(CardState.Idle);
    }

    public void Select()
    {
        Move(CardState.Selected);
    }

    public void Hide()
    {
        Move(CardState.Hidden);
    }

    public void Flip()
    {
        if (isFlipped)
        {
            StartCoroutine(FlipR());
            isFlipped = false;
        }
        else
        {
            StartCoroutine(FlipH());
            isFlipped = true;
        }
    }

    public virtual void FlipReveal()
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
        float fliptime = 0.2f; // half of total flip time
        while (time < fliptime) {
            display.transform.localScale = new Vector3(1 - time / fliptime, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        display.transform.localScale = new Vector3(0, 1, 1);
        foreach (Transform child in display.transform) {
            child.gameObject.SetActive(true);
        }
        time = 0;
        while (time < fliptime) {
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
        float fliptime = 0.2f;
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

    public void StartDestroy()
    {
        if (slot)
        {
            CardSlot real = slot.GetComponent<CardSlot>();
            if (real) real.ForgetCard();
        }
        Destroy(gameObject);
    }
}
