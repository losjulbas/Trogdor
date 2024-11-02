using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int score = 0;


    public void AddScore(int toAdd)
    {
        score += toAdd;
        UpdaScoreText();
    }

    void UpdaScoreText()
    {


        var paddedScore = score.ToString().PadLeft(8, '0');

        scoreText.text = "Score:\n" + paddedScore; //+"\nHighscore: " + paddedHighScore;
    }
}
