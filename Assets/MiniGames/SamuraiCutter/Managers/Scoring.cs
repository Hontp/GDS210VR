using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public string scoreText;

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
}
