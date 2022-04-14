using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int totalLevel;

    private void Awake()
    {
        totalLevel = SceneManager.sceneCountInBuildSettings;
    }

    public void RestartGame()
    {
        GameManager.instance.uiManager.gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        GameManager.instance.uiManager.gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % totalLevel);
    }

    public int GetCurrentLevelNo()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }

    public int GetNextLevelNo()
    {
        return ((SceneManager.GetActiveScene().buildIndex + 1) % totalLevel) + 1;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
