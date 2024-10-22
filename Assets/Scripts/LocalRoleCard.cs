using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalRoleCard : MonoBehaviour
{
    public int index = 0;
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 raised, hovered, def, hidden;
    private int state = 0;
    // 0 = default
    // 1 = hovered
    // 2 = selected
    // 3 = hidden

    // Start is called before the first frame update
    void Start()
    {
        raised = gameObject.transform.parent.GetChild(0).transform.localPosition;
        hovered = gameObject.transform.parent.GetChild(1).transform.localPosition;
        def = gameObject.transform.parent.GetChild(2).transform.localPosition;
        hidden = gameObject.transform.parent.GetChild(3).transform.localPosition;
    }

    void OnMouseEnter()
    {
        if (state == 0 || state == 1)
        {
            state = 1;
            target = hovered;
            Move();
        }
    }

    void OnMouseExit()
    {
        if (state == 0 || state == 1)
        {
            state = 0;
            target = def;
            Move();
        }
    }

    //on click
    void OnMouseDown()
    {
        if (state == 1)
        {
            Select();
            return;
        }
        if (state == 2)
        {
            GameManager.localInstance.SelectRole(-1);
            Deselect();
            return;
        }
    }

    private void Select()
    {
        state = 2;
        target = raised;
        Move();
        GameManager.localInstance.SelectRole(index);
        GameObject localcards = transform.parent.parent.gameObject;
        for (int i = 0; i < localcards.transform.childCount; i++)
        {
            if (i != index)
            {
                localcards.transform.GetChild(i).GetChild(4).GetComponent<LocalRoleCard>().Deselect();
            }
        }
    }

    public void Deselect()
    {
        state = 0;
        target = def;
        Move();
    }

    public void Hide()
    {
        state = 3;
        target = hidden;
        Move();
    }


    public void Move()
    {
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
