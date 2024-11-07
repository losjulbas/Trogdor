using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] int highscore;
    [SerializeField] int score = 0;
    [SerializeField] TMP_Text visualScoreText;
    private Vector3 initialPosition;
    private int accumulatedDamage = 0;
    private bool isDisplayingDamage = false;


    private void Start()
    {
        initialPosition = visualScoreText.rectTransform.localPosition;
        highscore = PlayerPrefs.GetInt("HighScore1", 0);
        score = 0; //restart 
    }
    public void AddScore(int toAdd)
    {
        score += toAdd;
        accumulatedDamage += toAdd;
        if (!isDisplayingDamage)
        {
            StartCoroutine(DisplayAccumulatedDamage());
        }
        UpdaScoreText();
    }

    private IEnumerator DisplayAccumulatedDamage()
    {
        isDisplayingDamage = true;
        yield return new WaitForSeconds(0.5f);  // Delay before showing accumulated damage

        visualScoreText.text = "+" + accumulatedDamage;
        StartCoroutine(AnimateText());

        accumulatedDamage = 0;
        isDisplayingDamage = false;
    }

    private IEnumerator AnimateText()
    {
        Vector3 targetPosition = initialPosition + new Vector3(0, 20, 0); // Move up by 20 units
        Color originalColor = visualScoreText.color;

        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            visualScoreText.rectTransform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            visualScoreText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        visualScoreText.text = "";
        visualScoreText.rectTransform.localPosition = initialPosition;
        visualScoreText.color = originalColor; // Reset color for next use
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
        scoreText.text = "Score: " + paddedScore;
        highScoreText.text = "Highscore: " + paddedHighScore;
    }
}
