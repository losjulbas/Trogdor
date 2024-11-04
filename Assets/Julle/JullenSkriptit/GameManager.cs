using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text roundTimerText;
    ScuffedDragon scuffedDragon;
    public List<PowerupType> powerups;
    [SerializeField] private float roundTimer;
    [SerializeField] private float gameOverDelay = 1f;
    SimpleAudioSource audioSource;
    public GameObject gameOverScreen;
    public GameObject creditsScreen;
    public AudioSource flapSource;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        audioSource = FindAnyObjectByType<SimpleAudioSource>();
        roundTimer = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        roundTimer += Time.deltaTime;
    }

    void GameOver(bool outofLives)
    {
        if (outofLives)
        {

            gameOverText.text = "You died!\nPress R to restart";
            Debug.Log("You lost!");
        }
        else
        {
            gameOverText.text = "You won!\nPress R to restart";
            Debug.Log("You won!");
        }

        gameOverScreen.SetActive(true);

        flapSource.enabled = false;
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(roundTimer / 60);  // Convert total seconds to minutes
        int seconds = Mathf.FloorToInt(roundTimer % 60);  // Get remaining seconds

        // Display the time in "mm:ss" format
        roundTimerText.text = "Round Time: " + minutes.ToString("0") + ":" + seconds.ToString("00");

        Time.timeScale = 0;
        //TODO: game over UI
    }

    public void GameWon()
    {
        StartCoroutine(DelayedGameOver(false));
    }

    public void GameLost()
    {
        GameOver(true);
        //StartCoroutine(DelayedGameOver(true));
    }

    private IEnumerator DelayedGameOver(bool outofLives)
    {
        yield return new WaitForSeconds(gameOverDelay);
        //audioSource.PlaySound("Trumpets");
        GameOver(outofLives);
    }


    public void PowerupActivated(PowerupType whichType)
    {
        if (whichType == PowerupType.Armor)
        {

            scuffedDragon.PowerupActivated(PowerupType.Armor);

        }
        else
        {
            Debug.LogError("unknown powerup type, can't handle");
        }
    }

    public void RestartButton() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void CreditsButton()
    {
        creditsScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void BackToMainMenuFromCredits()
    {
        creditsScreen.SetActive(false);
        gameOverScreen.SetActive(true); ;
    }
    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Exits play mode in the editor
#else
        Application.Quit();  // Exits the application when running as a build
#endif
    }
    public void ClearDataButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All saved data has been cleared.");
    }
}
