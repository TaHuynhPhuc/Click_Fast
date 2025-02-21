using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    public void GoToGamePlay()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
    public void GoToLookBack()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        UnityEngine.SceneManagement.SceneManager.LoadScene("LookBackQuestion");
    }

    public void GoToBXH()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        UnityEngine.SceneManagement.SceneManager.LoadScene("BXH");
    }
}
