using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiCutter;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public string scoreText;
    public bool gameOver;

    private void Start()
    {
        currentScore = 0;
    }

    public void ScoringSystem()
    {
        currentScore += 1;
    }

    private void Update()
    {
        if(GameManager._instance.dead)
        {
            if (gameOver == false)
            {
                gameOver = true;
                CompareScore();
            }
        }
        else
        {
            gameOver = false;
        }
    }

    void CompareScore()
    {
        if (PlayerPrefs.GetInt("SC_Highscore_Num") < currentScore)
        {
            PlayerPrefs.SetInt("SC_Highscore_Num", currentScore);
            PlayerPrefs.SetString("SC_Highscore", "HIGHSCORE: " + currentScore.ToString());
        }
        scoreText = PlayerPrefs.GetString("SC_Highscore");
    }
}
