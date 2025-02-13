using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup mainUI;
    public GameObject settingsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainUI.alpha = 0.5f;
        mainUI.interactable = false;
        mainUI.blocksRaycasts = false;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainUI.alpha = 1f;
        mainUI.interactable = true;
        mainUI.blocksRaycasts = true;
    }
}
