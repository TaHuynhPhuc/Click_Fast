using UnityEngine;

public class SceneManagefr : MonoBehaviour
{
    public string nameScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
    }
}
