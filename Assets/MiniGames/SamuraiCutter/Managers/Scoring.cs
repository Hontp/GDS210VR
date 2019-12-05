using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiCutter;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public int highscore;
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


        if (!hasScore)
        {
            if (PlayerPrefs.HasKey("SC_Highscore"))
            {
                highscore = PlayerPrefs.GetInt("SC_Highscore");

            }
            else
            {
                PlayerPrefs.SetInt("SC_Highscore", 0);
                highscore = 0;
            }

            scoreText = highscore.ToString();
            hasScore = true;
        }
    }

    void CompareScore()
    {
        if (highscore < currentScore)
        {
            PlayerPrefs.SetInt("SC_Highscore", currentScore);
            highscore = currentScore;
           
        }
        scoreText = highscore.ToString();
    }
}
