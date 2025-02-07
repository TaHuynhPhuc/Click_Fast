using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class QuestManager : MonoBehaviour
{
    

    public int score;
    public Question currentQuestion;
    public List<Question> questions = new List<Question>();

    public List<int> passIndex = new List<int>();
    public List<int> selectIndex = new List<int>();

    [Space]
    [Header("Time")]
    [SerializeField]
    private float timeOut;
    private bool gameOver;

    [Space]
    [Header("UI")]
    public TextMeshProUGUI question;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;

    public TextMeshProUGUI timeOutUI;

    public void UpdateQuestUI()
    {
        question.text = currentQuestion.question;
        answer1.text = currentQuestion.answers[0];
        answer2.text = currentQuestion.answers[1];
        answer3.text = currentQuestion.answers[2];
        answer4.text = currentQuestion.answers[3];
    }

    private static QuestManager _instance;

    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Tìm kiếm instance trong scene
                _instance = FindObjectOfType<QuestManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
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

    private void Start()
    {
        currentQuestion = GetRandomQuestion();
        UpdateQuestUI();
    }
    private void Update()
    {
        CountDown();
    }
    public Question GetRandomQuestion()
    {
        // Kiểm tra nếu đã hỏi tất cả các câu hỏi
        if (passIndex.Count >= questions.Count)
        {
            Debug.Log("Không còn câu hỏi nào nữa!");

            return null; // Không còn câu hỏi nào để hỏi
        }

        int randomIndex;
        do
        {
            
            randomIndex = Random.Range(0, questions.Count);
        } while (passIndex.Contains(randomIndex)); // Tiếp tục cho đến khi tìm thấy câu hỏi chưa hỏi

        passIndex.Add(randomIndex); // Thêm chỉ số câu hỏi vào danh sách đã hỏi
        return questions[randomIndex]; // Trả về câu hỏi ngẫu nhiên chưa hỏi
    }

    public void CountDown()
    {
        if(timeOut > 0)
        {
            timeOut -= Time.deltaTime;
        } 
        if(timeOut <= 0)
        {
            gameOver = true;
        }
        timeOutUI.text = Mathf.Round(timeOut).ToSafeString();
    }

    public void LoadNextQuestion()
    {
        currentQuestion = GetRandomQuestion();
        if (currentQuestion != null)
        {
            UpdateQuestUI();
        }
    }

    public void OnAnswerSelected(int index)
    {
        if(currentQuestion.correctAnswerIndex == 0 || gameOver)
        {
            return;
        }
        selectIndex.Add(index);
        if (index == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Dung");
            score += 20;
            LoadNextQuestion();

        }
        else
        {
            Debug.Log("Sai");
            Debug.Log("Bạn đã thi trượt");
            gameOver = true ;
        }
       
    }
}

