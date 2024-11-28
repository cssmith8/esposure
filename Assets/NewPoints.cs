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
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialEuler = transform.eulerAngles;
        Vector3 initialPosition = transform.position;

        Vector3 midScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 midEuler = new Vector3(0, 0, -2);
        Vector3 midPosition = transform.position;

        Vector3 finalScale = new Vector3(0, 0, 0);
        Vector3 finalEuler = new Vector3(0, 0, -160);
        Vector3 finalPosition = target.position;
        finalPosition.z = -2;

        float elapsedTime = 0;
        float speed1 = 4f;
        // move from initial to mid, eased using a sine function over 1 second
        while (elapsedTime < 1f / speed1)
        {
            float progress = Mathf.Sin(elapsedTime * speed1 * Mathf.PI / 2);
            transform.localScale = Vector3.Lerp(initialScale, midScale, progress);
            transform.eulerAngles = Vector3.Lerp(initialEuler, midEuler, progress);
            transform.position = Vector3.Lerp(initialPosition, midPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //wait
        yield return new WaitForSeconds(0.75f);

        // move from mid to final, eased using a sine function over 1 second
        elapsedTime = 0;
        float speed2 = 1.5f;
        while (elapsedTime < 1 / speed2)
        {
            float progress = Mathf.Pow(Mathf.Sin((1 / speed2 - elapsedTime) * speed2 * Mathf.PI / 2), 2);
            transform.localScale = Vector3.Lerp(finalScale, midScale, progress);
            transform.eulerAngles = Vector3.Lerp(finalEuler, midEuler, progress);
            transform.position = Vector3.Lerp(finalPosition, midPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        if (local)
        {
            Scoreboard.instance.AddToLocal(points);
        } else
        {
            Scoreboard.instance.AddToEnemy(points);
        }
        Destroy(gameObject);
    }
}
