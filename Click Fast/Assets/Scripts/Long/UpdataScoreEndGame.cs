using System.Linq;
using TMPro;
using UnityEngine;

public class UpdataScoreEndGame : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBestScore;

    void Start()
    {
        textScore.text = QuestController.Instance.score.ToString();
        textBestScore.text = DatabaseManager.Instance.GetBestScore().ToString();
    }

   
}
