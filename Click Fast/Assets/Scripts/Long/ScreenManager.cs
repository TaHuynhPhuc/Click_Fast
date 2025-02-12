using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;


    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Tìm kiếm instance trong scene
                instance = FindAnyObjectByType<ScreenManager>();
            }
            return instance;
        }
        set
        {
            if (instance == null)
            {
                // Tìm kiếm instance trong scene
             
            }
        }
    }
    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance tồn tại
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int sceneIndex = 0;
    void Start()
    {
        sceneIndex = SceneManager.loadedSceneCount;
    }


    public void LoadLookBackScene()
    {
        SceneManager.LoadScene("LookBackQuestion"); 
    }
    public void LoadEndScene()
    {
        SceneManager.LoadScene("KetThuc");
    }
   
}

