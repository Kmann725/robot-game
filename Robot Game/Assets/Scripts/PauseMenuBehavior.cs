using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : Singleton<PauseMenuBehavior>
{
    public TextMeshProUGUI textbox;
    public GameObject resumeButton;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (GameController.Instance.gameLost)
        {
            textbox.text = "You Lost!";
            resumeButton.SetActive(false);
        }
        else
        {
            textbox.text = "Game Paused";
            resumeButton.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        ScoreManager.Instance.PlayerRestarted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title Menu");
    }
}
