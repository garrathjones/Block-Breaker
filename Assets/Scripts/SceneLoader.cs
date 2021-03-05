using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // cached reference
    GameSession gameStatus;

    public void LoadNextScene()
    {
        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScenceIndex + 1);
    }

    public void LoadStartScene()
    {
        int currentScenceIndex = 0;
        SceneManager.LoadScene(currentScenceIndex);
        FindObjectOfType<GameSession>().ResetScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
