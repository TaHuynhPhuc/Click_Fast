using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ReviewQuest : MonoBehaviour
{
    [Space]
    [Header("UI")]
    public TextMeshProUGUI question;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;


 

    public int currentSelectIndex;
    public int currentPassIndex;
    public Question currentQuestion;

    private void Start()
    {
        currentSelectIndex = QuestManager.Instance.selectIndex[0];
        currentPassIndex = QuestManager.Instance.passIndex[0];
        currentQuestion = QuestManager.Instance.questions[currentPassIndex];
        CheckingAnswer();
    }

    public void CheckingAnswer()
    {
       
        if (currentSelectIndex == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Dung");

        }
        else
        {
            Debug.Log("Sai");
            Debug.Log("Bạn đã thi trượt");         
        }
    }

    
}
