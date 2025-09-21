using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int applesCaught = 0;
    public TMP_Text scoreText;       // your current score text
    public TMP_Text highScoreText;   // drag your highScoreText TMP object here

    private int highScore;

    void Start()
    {
        applesCaught = 0;

        // Load high score from PlayerPrefs (default 0 if not set)
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreUI();
    }

    public void AddApple()
    {
        applesCaught++;
        UpdateScoreUI();

        // Check if we have a new high score
        if (applesCaught > highScore)
        {
            highScore = applesCaught;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = applesCaught.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
