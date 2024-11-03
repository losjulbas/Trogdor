using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text gameOverText;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

        Time.timeScale = 0;
        //TODO: game over UI
    }

    public void GameWon()
    {

        GameOver(true);
    }

    public void GameLost()
    {
        GameOver(false);
    }
}
