using UnityEngine;

public class SceneManagefr : MonoBehaviour
{
    public void GoToGameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
