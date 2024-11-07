using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCanvas : MonoBehaviour
{
    [SerializeField] private GameObject localScore;
    [SerializeField] private GameObject enemyScore;
    [HideInInspector] public static ScoreCanvas instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateLocalScore(int amount)
    {
        localScore.GetComponent<TMP_Text>().text = amount.ToString();
    }

    public void UpdateEnemyScore(int amount)
    {
        enemyScore.GetComponent<TMP_Text>().text = amount.ToString();
    }
}
