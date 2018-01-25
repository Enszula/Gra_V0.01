using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public GameObject PauseUI;

    private bool zapauzowana = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            zapauzowana = !zapauzowana;
        }

        if (zapauzowana)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (!zapauzowana)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void Resume()
    {
        zapauzowana = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
