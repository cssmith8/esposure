using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRoleCard : MonoBehaviour
{
    public int index = 0;
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 raised, def, hidden;
    private int state = 0;
    // 0 = default
    // 1 = hovered
    // 2 = selected
    // 3 = hidden

    // Start is called before the first frame update
    void Start()
    {
        raised = gameObject.transform.parent.GetChild(0).transform.localPosition;
        def = gameObject.transform.parent.GetChild(1).transform.localPosition;
        hidden = gameObject.transform.parent.GetChild(2).transform.localPosition;
    }

    public void Move(bool selected)
    {
        if (selected)
        {
            target = raised;
            state = 2;
        }
        else
        {
            target = def;
            state = 0;
        }
        StopCoroutine(MoveToTarget());
        StartCoroutine(MoveToTarget());
    }

    //coroutine to move the card up
    IEnumerator MoveToTarget()
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
