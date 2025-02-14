using UnityEngine;

public class SettingsUIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        AudioManager.Instance.PlaySoundEffect("click");
        settingsPanel.SetActive(true);
        //mainButtons.SetActive(false);
    }

    public void CloseSettings() 
    {
        AudioManager.Instance.PlaySoundEffect("click");
        settingsPanel.SetActive(false);
        //mainButtons.SetActive(true);
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive); // Đảo trạng thái của panel
        }
    }
}
