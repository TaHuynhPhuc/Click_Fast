using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Chuyển đến màn chơi chính
    public void LoadGame()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        SceneManager.LoadScene("LookBackQuestion");
    }

    // Chuyển đến bảng xếp hạng
    public void LoadLeaderboard()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        SceneManager.LoadScene("BXH");
    }

    // Quay về màn hình chính
    public void LoadMainMenu()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        SceneManager.LoadScene("Start");
    }

    // Thoát game
    public void QuitGame()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

}
