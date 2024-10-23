using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using CS = CardState;

public enum CardState {
    Idle = 0,
    Hovered = 1,
    Selected = 2,
    Hidden = 3,
}

public abstract class CardSlot : MonoBehaviour
{
    public int slotID = 0;
    protected bool IsLocal;
    protected Vector3 target = Vector3.zero;
    [SerializeReference] protected GameObject objRaised, objHovered, objIdle, objHidden;
    protected Vector3 raised, hovered, idle, hidden = Vector3.zero;
    protected CardState state = CS.Idle;

    // Start is called before the first frame update
    protected virtual void Start() {
        raised = objRaised.transform.localPosition;
        hovered = objHovered.transform.localPosition;
        idle = objIdle.transform.localPosition;
        hidden = objHidden.transform.localPosition;
    }

    protected void Move()
    {
        StopCoroutine(MoveToTarget());
        StartCoroutine(MoveToTarget());
    }

    //coroutine to move the card up
    protected IEnumerator MoveToTarget()
    {
        float time = Time.timeSinceLevelLoad;
        while (Vector3.Distance(transform.localPosition, target) > 0.1f)
        {
            while (Time.timeSinceLevelLoad - time < Time.fixedDeltaTime) yield return null; //lock this to 60fps
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, 0.08f);
            yield return null;
        }
    }
}
