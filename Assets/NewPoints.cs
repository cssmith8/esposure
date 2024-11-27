using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewPoints : MonoBehaviour
{
    [SerializeField] public int points = 0;
    [SerializeField] private bool local = true;
    [HideInInspector] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        if (local)
        {
            target = Scoreboard.instance.GetLocalTarget();
        } else
        {
            target = Scoreboard.instance.GetEnemyTarget();
        }
        transform.eulerAngles = new Vector3(0, 0, 90);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialEuler = transform.eulerAngles;
        Vector3 initialPosition = transform.position;

        Vector3 midScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 midEuler = new Vector3(0, 0, 0);
        Vector3 midPosition = transform.position;

        Vector3 finalScale = new Vector3(0, 0, 0);
        Vector3 finalEuler = new Vector3(0, 0, -160);
        Vector3 finalPosition = target.position;

        float elapsedTime = 0;
        // move from initial to mid, eased using a sine function over 1 second
        while (elapsedTime < 0.5)
        {
            transform.localScale = Vector3.Lerp(initialScale, midScale, Mathf.Sin(elapsedTime * 2 * Mathf.PI / 2));
            transform.eulerAngles = Vector3.Lerp(initialEuler, midEuler, Mathf.Sin(elapsedTime * 2 * Mathf.PI / 2));
            transform.position = Vector3.Lerp(initialPosition, midPosition, Mathf.Sin(elapsedTime * 2 * Mathf.PI / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //wait
        yield return new WaitForSeconds(0.5f);

        // move from mid to final, eased using a sine function over 1 second
        elapsedTime = 0;
        while (elapsedTime < 1)
        {
            transform.localScale = Vector3.Lerp(midScale, finalScale, Mathf.Sin(1 - ((1 - elapsedTime) * Mathf.PI / 2)));
            transform.eulerAngles = Vector3.Lerp(midEuler, finalEuler, Mathf.Sin(1 - ((1 - elapsedTime) * Mathf.PI / 2)));
            transform.position = Vector3.Lerp(midPosition, finalPosition, Mathf.Sin(1 - ((1 - elapsedTime) * Mathf.PI / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Destroy(gameObject);
    }
}
