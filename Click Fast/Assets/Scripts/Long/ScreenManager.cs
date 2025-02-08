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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneNext()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex); 
    }
    public void LoadSceneBefore()
    {
        sceneIndex--;
        SceneManager.LoadScene(sceneIndex);
    }
}
