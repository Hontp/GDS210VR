using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiCutter;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public string scoreText;
    public bool gameOver, hasScore;

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

        if(PlayerPrefs.HasKey("SC_Highscore"))
        {
            hasScore = true;
        }
        else
        {
            PlayerPrefs.SetInt("SC_Highscore_Num", 0);
            PlayerPrefs.SetString("SC_Highscore", "HIGHSCORE: " + PlayerPrefs.GetInt("SC_Highscore_Num"));
            hasScore = true;
        }
    }

    void CompareScore()
    {
        if (PlayerPrefs.GetInt("SC_Highscore_Num") < currentScore && hasScore)
        {
            PlayerPrefs.SetInt("SC_Highscore_Num", currentScore);
            PlayerPrefs.SetString("SC_Highscore", "HIGHSCORE: " + currentScore.ToString());
        }
        scoreText = PlayerPrefs.GetString("SC_Highscore");
    }
}
