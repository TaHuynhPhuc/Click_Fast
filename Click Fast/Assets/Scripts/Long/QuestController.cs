using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<int> passIndex = new List<int>();
    public List<string> selectAnswer = new List<string>();
    public int score;
    private static QuestController _instance;

    public static QuestController Instance
    {
        get
        {
            if (_instance == null)
            {
                // Tìm kiếm instance trong scene
                _instance = FindAnyObjectByType<QuestController>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        //  FindTextMPro();
        // Đảm bảo rằng chỉ có một instance tồn tại
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
