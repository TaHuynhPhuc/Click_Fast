using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    public void GoToGamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    public void GoToBXH()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BXH");
    }
}
