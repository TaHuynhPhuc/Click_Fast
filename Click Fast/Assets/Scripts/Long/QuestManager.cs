using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class QuestManager : MonoBehaviour
{
    

    public int score;
    public Question currentQuestion;
    public List<Question> questions = new List<Question>();

    public List<int> passIndex = new List<int>();
    public List<string> selectAnswer = new List<string>();

    [Space]
    [Header("Time")]
    [SerializeField]
    private float timeOut;
    private bool gameOver;

    [Space]
    [Header("UI")]
    public TextMeshProUGUI question;
    public Image questionImage;
    public List<TextMeshProUGUI> answerList;

    public GameObject MenuLoss;
  /*  public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;
  */

    public TextMeshProUGUI timeOutUI;

    public void UpdateQuestUI()
    {
        
        question.text = currentQuestion.question;
        questionImage.sprite = currentQuestion.questImage;

        answerList[0].text = currentQuestion.answers[0];
        answerList[1].text = currentQuestion.answers[1];
        answerList[2].text = currentQuestion.answers[2];
        answerList[3].text = currentQuestion.answers[3];
        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            if (answerList[i].text == "")
            {
            //   Debug.Log("nulll");
               answerList[i].transform.parent.gameObject.SetActive(false);
            } else
            {
                answerList[i].transform.parent.gameObject.SetActive(true);
              //  Debug.Log("not nulll");
            }
        }
       
    }

    private static QuestManager _instance;

    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Tìm kiếm instance trong scene
                _instance = FindAnyObjectByType<QuestManager>();
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
        if (ScreenManager.Instance.sceneIndex != 1)
        {
            return;
        }
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
            if (ScreenManager.Instance != null)
            {
                ScreenManager.Instance.LoadSceneNext();
            } else
            {
                Debug.Log("nulll rồi con");
            }
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

            MenuLoss.SetActive(true);
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
        if (gameOver || currentQuestion.correctAnswer == null)
        {
            return;
        } 

        selectAnswer.Add(chooseAswer(index)); 

        if( currentQuestion.correctAnswer == chooseAswer(index))
        {
                Debug.Log("Dung");
                score += 20;
                LoadNextQuestion();
               
        } else
        {
                Debug.Log("Sai");
                Debug.Log("Bạn đã thi trượt");
                gameOver = true;
                MenuLoss.SetActive(true);
        } 
    }

    string myAnswer;
    public string chooseAswer(int index)
    {
        Debug.Log("chosseIndex " + index);
    
        switch(index)
        {
            case 1:
                myAnswer = answerList[0].text;
               
                break;
            case 2:
                myAnswer = answerList[1].text;
                break;
            case 3:
                myAnswer = answerList[2].text;
                break;
            case 4:
                myAnswer = answerList[3].text;
                break;
        }
       /// Debug.Log(myAnswer);
        return myAnswer;
        
    }
    
}

