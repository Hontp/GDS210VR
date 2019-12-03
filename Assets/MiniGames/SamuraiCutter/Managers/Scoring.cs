using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public string scoreText;
    PlayerPrefs SC_HighScore_1, SC_HighScore_2, SC_HighScore_3;


    private void Start()
    {
        currentScore = 0;
        scoreText = "SCORE: " + currentScore;
    }

    public void ScoringSystem()
    {
        currentScore += 1;
        scoreText = "SCORE: " + currentScore;
    }

    void CompareScore()
    {
        //if(currentScore >= SC_HighScore_1)
        //{

        //}
    }
}
