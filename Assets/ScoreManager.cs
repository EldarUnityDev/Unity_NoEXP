using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;

    private void Awake()
    {
        References.scoreManager = this;
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            References.canvas.highScoreText.text = highScore.ToString();
            //save it to disc
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        References.canvas.scoreText.text = score.ToString();
    }
}
