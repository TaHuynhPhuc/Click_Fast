using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Chuyển đến màn chơi chính
    public void LoadGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // Chuyển đến bảng xếp hạng
    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("BXH");
    }

    // Quay về màn hình chính
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Thoát game
    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}
