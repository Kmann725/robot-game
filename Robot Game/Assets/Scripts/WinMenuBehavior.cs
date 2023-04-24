using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuBehavior : Singleton<WinMenuBehavior>
{
    public TextMeshProUGUI winText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winText.text = $"You Saved The World!\nYou Earned {ScoreManager.Instance.GetStarsEarned()}/3 Stars";
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartLevel()
    {
        Destroy(ScoreManager.Instance.gameObject);
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title Menu");
    }
}
