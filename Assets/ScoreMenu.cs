using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    public TextMeshProUGUI verdictText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        if(References.thePlayer == null)
        {
            verdictText.text = "DEAD";

        }
        else
        {
            verdictText.text = "VICTORY";
        }
        scoreText.text = References.scoreManager.score.ToString("N0");
        highScoreText.text = References.scoreManager.highScore.ToString("N0");

    }
}
