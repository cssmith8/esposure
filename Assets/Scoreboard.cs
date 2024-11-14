using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [HideInInspector] public static Scoreboard instance;
    [SerializeField] private GameObject orangeFlame, blueFlame;
    [SerializeField] private GameObject localScoreText, enemyScoreText;
    private int localScore = 0, enemyScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateFlames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }  

    public int GetLocalScore() { return localScore; }

    public int GetEnemyScore() { return enemyScore; }

    public void UpdateScore(int local, int enemy)
    {
        localScore = local;
        enemyScore = enemy;
        localScoreText.GetComponent<TMP_Text>().text = localScore.ToString();
        enemyScoreText.GetComponent<TMP_Text>().text = enemyScore.ToString();
        UpdateFlames();
    }

    private void UpdateFlames()
    {
        orangeFlame.SetActive(localScore < enemyScore);
        blueFlame.SetActive(localScore > enemyScore);
    }
}
