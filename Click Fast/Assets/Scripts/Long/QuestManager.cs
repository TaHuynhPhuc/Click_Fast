using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System.Threading;
using System.Collections;
using System;

public class QuestManager : MonoBehaviour
{
    [Header("Tai Nguyen Can Them")]
    public GameObject dataObject;
    public Sprite spriteCorrect;
    public Sprite spriteIncorrect;
    public Sprite spriteNormal;


    public int score;
    public Question currentQuestion;
    public List<Question> questions = new List<Question>();



    [Space]
    [Header("Time")]
    [SerializeField]
    private float timeOut;
   // private bool gameOver;

    [Space]
    [Header("UI")]
    public TextMeshProUGUI question;
    public Image questionImage;
    public List<TextMeshProUGUI> answerList;
    public TextMeshProUGUI timeOutUI;

    public GameObject oneChossePerTime;

    // public GameObject MenuLoss;
    /*  public TextMeshProUGUI answer1;
      public TextMeshProUGUI answer2;
      public TextMeshProUGUI answer3;
      public TextMeshProUGUI answer4;
    */

  

    public void UpdateQuestUI()
    {
        ResetUIButton();
        question.text = currentQuestion.question;
            questionImage.sprite = currentQuestion.questImage;
            for (int i = 0; i < currentQuestion.answers.Length; i++)
             {
                answerList[i].text = currentQuestion.answers[i];
             }
             if(currentQuestion.answers.Length == 3)
             {
             answerList[3].transform.parent.gameObject.GetComponent<Image>().enabled = false;
             answerList[3].GetComponent<TextMeshProUGUI>().enabled = false;
             }
                 else
                {
                     answerList[3].transform.parent.gameObject.GetComponent<Image>().enabled = true;
                     answerList[3].GetComponent<TextMeshProUGUI>().enabled = true;
                }

        /*
         for (int i = 0; i < currentQuestion.answers.Length; i++)
       {
           if (answerList[i].text == "" || answerList[i] == null)
           {
               answerList[i].transform.parent.gameObject.GetComponent<Image>().enabled = false;
               answerList[i].GetComponent<TextMeshProUGUI>().enabled = false;

            }
           else
           {
               answerList[i].transform.parent.gameObject.GetComponent<Image>().enabled = true;
               answerList[i].GetComponent<TextMeshProUGUI>().enabled = true;
           }
       }
         */
    }



    private void Start()
    {
        QuestController.Instance.passIndex.Clear();
        QuestController.Instance.selectAnswer.Clear();
        FindTextMPro();
        questions = dataObject.GetComponent<DataQuestion>().QuestionList;
        if (SceneManager.GetActiveScene().name != "GamePlay")
        {
            return;
        }
        currentQuestion = GetRandomQuestion();
        UpdateQuestUI();
        // OnReloadScene();    
    }

    public void OnReloadScene()
    {
        if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            currentQuestion = GetRandomQuestion();
            FindTextMPro();
            UpdateQuestUI();
           
        } else
        {
            Debug.Log("Flase");
        }
       
    }

    public void FindTextMPro() {
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            timeOutUI = GameObject.Find("Timmer").GetComponent<TextMeshProUGUI>();
            question = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
            questionImage = GameObject.Find("ImageQuest").GetComponent<Image>();
            answerList[0] = GameObject.Find("Answer1Text").GetComponent<TextMeshProUGUI>();
            answerList[1] = GameObject.Find("Answer2Text").GetComponent<TextMeshProUGUI>();
            answerList[2] = GameObject.Find("Answer3Text").GetComponent<TextMeshProUGUI>();
            answerList[3] = GameObject.Find("Answer4Text").GetComponent<TextMeshProUGUI>();
        }   
        }
    private void Update()
    {
        CountDown();
    }
    public Question GetRandomQuestion()
    {
        // Kiểm tra nếu đã hỏi tất cả các câu hỏi
        if (QuestController.Instance.passIndex.Count >= questions.Count)
        {
            if (ScreenManager.Instance != null)
            {
              //  Debug.Log("load lokback");
               ScreenManager.Instance.LoadLookBackScene();
            } else
            {
                Debug.Log("nulll rồi");
            }
            return null; // Không còn câu hỏi nào để hỏi
        }

        int randomIndex;
        do
        {       
            randomIndex =UnityEngine.Random.Range(0, questions.Count);
        } while (QuestController.Instance.passIndex.Contains(randomIndex)); // Tiếp tục cho đến khi tìm thấy câu hỏi chưa hỏi

        QuestController.Instance.passIndex.Add(randomIndex); // Thêm chỉ số câu hỏi vào danh sách đã hỏi
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
            //gameOver = true;
            QuestController.Instance.passIndex.Clear();
            QuestController.Instance.selectAnswer.Clear();
            ScreenManager.Instance.LoadEndScene();

        }
        if(timeOutUI == null)
        {
          //FindTextMPro();
        }else
        {
            timeOutUI.text = Mathf.Round(timeOut).ToSafeString();
        }
         
    }

    public void LoadNextQuestion()
    {
        currentQuestion = GetRandomQuestion();
        if (currentQuestion != null)
        {
            OnChossePerTime(false);
            UpdateQuestUI();
        }
    }

    public void ResetUIButton()
    {
        foreach (TextMeshProUGUI answer in answerList)
        {
            answer.transform.parent.GetComponent<Image>().sprite = spriteNormal;
        }

    }
  //  bool isChosse;
    public void OnChossePerTime( bool isChosse)
    {
        if (isChosse)
        {
            oneChossePerTime.SetActive(true);
        } else
        {
            oneChossePerTime.SetActive(false);
        }
      
    }
    public void OnAnswerSelected(int index)
    {
       
        OnChossePerTime(true);
        if (currentQuestion.correctAnswer == null) //gameOver || 
        {
            return;
        }

        QuestController.Instance.selectAnswer.Add(chooseAswer(index)); 

        if( currentQuestion.correctAnswer == chooseAswer(index))
        {
            score += 10;
            //Debug.Log(index);
            answerList[index-1].transform.parent.GetComponent<Image>().sprite =  spriteCorrect;
            StartCoroutine(delayShowAnswer());
               
        } else
        {
            //    gameOver = true; 
            answerList[index - 1].transform.parent.GetComponent<Image>().sprite = spriteIncorrect;
            Debug.Log("sai"); 
            StartCoroutine(delayShowFailAnswer());
  
        } 
    }

    public void EndGameAddScore()
    {
     // DatabaseManager.Instance.playerData.
    }

    public IEnumerator delayShowFailAnswer()
    {
        yield return new WaitForSeconds(1f);
        ScreenManager.Instance.LoadEndScene();
    }

    public  IEnumerator delayShowAnswer()
    {
        yield return new WaitForSeconds(1f);
        LoadNextQuestion();
    }

    string myAnswer;
    public string chooseAswer(int index)
    {
       // Debug.Log("chosseIndex " + index);
    
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

