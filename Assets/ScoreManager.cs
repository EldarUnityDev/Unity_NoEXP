using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public float defaultTimeToShowRecentScore;
    int recentScore;
    float secondsLeftToShowRecentScore;
    private void Awake()
    {
        References.scoreManager = this;
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        References.canvas.highScoreText.text = highScore.ToString();

    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        recentScore += amount;
        secondsLeftToShowRecentScore = defaultTimeToShowRecentScore;
        References.canvas.scoreText.text = score.ToString();
        References.canvas.recentScoreText.text = "+" + recentScore.ToString();

    }
    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            References.canvas.highScoreText.text = highScore.ToString();
            //save it to disc
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }
    private void Update()
    {
        secondsLeftToShowRecentScore -= Time.deltaTime;
        if(secondsLeftToShowRecentScore < 0)
        {
            recentScore = 0;
            References.canvas.recentScoreText.enabled = false;
        }
        else
        {
            References.canvas.recentScoreText.enabled = true;
        }
    }
}
