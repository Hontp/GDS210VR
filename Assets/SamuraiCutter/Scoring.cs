using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public int currentScore;
    public TMP_Text scoreText;

    private void Start()
    {
        currentScore = 0;
        scoreText.text = "SCORE: " + currentScore;
    }

    public void ScoringSystem()
    {
        currentScore += 1;
        scoreText.text = "SCORE: " + currentScore;
    }
}
