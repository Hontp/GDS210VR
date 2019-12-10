using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiCutter;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public int highscore_1;
    public string scoreText;
    public bool gameOver, hasScore;

    private void Start()
    {
        currentScore = 0;
        hasScore = false;
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
            if (PlayerPrefs.HasKey("SC_Highscore_1"))
            {
                highscore_1 = PlayerPrefs.GetInt("SC_Highscore_1");

            }
            else
            {
                PlayerPrefs.SetInt("SC_Highscore_1", 0);
                highscore_1 = 0;
            }
            
            scoreText = highscore_1.ToString();
            hasScore = true;
        }
    }

    void CompareScore()
    {

        if (highscore_1 < currentScore)
        {
            PlayerPrefs.SetInt("SC_Highscore_1", currentScore);
            highscore_1 = currentScore;

        }
        scoreText = "1st: " + highscore_1.ToString();
        PlayerPrefs.Save();
    }
    
}
