using UnityEngine;

public class SettingsUIManager : MonoBehaviour
{
    public GameObject settingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings() 
    {
        settingsPanel.SetActive(false);
    }
}
