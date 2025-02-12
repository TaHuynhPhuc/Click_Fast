using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    public void GoToGamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
    public void GoToLookBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LookBackQuestion");
    }

    public void GoToBXH()
    {
        //Load data mới nhất mỗi lần vào rank (Phúc Đẹp Trai)
        DatabaseManager.Instance.playerData.Clear();
        DatabaseManager.Instance.LoadTop30Players();
        UnityEngine.SceneManagement.SceneManager.LoadScene("BXH");
    }
}
