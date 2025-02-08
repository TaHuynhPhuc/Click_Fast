using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    public void GoToGamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
