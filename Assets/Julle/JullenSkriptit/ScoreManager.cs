using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] int highscore;
    [SerializeField] int score = 0;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore1", 0);
        UpdaScoreText();
    }
    public void AddScore(int toAdd)
    {
        score += toAdd;
        UpdaScoreText();
    }

    void UpdaScoreText()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore1", score);  // Save the new high score
            PlayerPrefs.Save();
        }

        var paddedScore = score.ToString().PadLeft(8, '0');
        var paddedHighScore = highscore.ToString().PadLeft(8, '0');

        // Show the current score and high score
        scoreText.text = "Score:\n" + paddedScore;
        highScoreText.text = "Highscore:\n" + paddedHighScore;
    }
}
