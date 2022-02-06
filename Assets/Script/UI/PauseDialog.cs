using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDialog : MonoBehaviour
{
    public GameObject pauseDialog;
    private bool status = false;
    public void Pause()
    {
        Time.timeScale = 0;
        pauseDialog.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseDialog.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        pauseDialog.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
