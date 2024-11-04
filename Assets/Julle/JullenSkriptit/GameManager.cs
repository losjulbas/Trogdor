using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text roundTimerText;
    ScuffedDragon scuffedDragon;
    public List<PowerupType> powerups;
    [SerializeField] private float roundTimer;

    private void Awake()
    {
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
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
            //TODO: Play audio

            gameOverText.text = "You died!\nPress R to restart";
        }
        else
        {
            //TODO: Play audio
            gameOverText.text = "You win!\nPress R to restart";
        }

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

        GameOver(false);
    }

    public void GameLost()
    {
        GameOver(true);
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
}
