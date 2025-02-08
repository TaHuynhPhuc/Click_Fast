using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ReviewQuest : MonoBehaviour
{
    [Space]
    [Header("UI")]
    public TextMeshProUGUI question;
    public Image questionImage;
    public List<TextMeshProUGUI> answerList;


    public int index = 0;

    public string currentSelectAnswer;
    public int currentPassIndex;
    public Question currentQuestion;

    private void Start()
    {
        currentSelectAnswer = QuestManager.Instance.selectAnswer[index];
        currentPassIndex = QuestManager.Instance.passIndex[index];
        currentQuestion = QuestManager.Instance.questions[currentPassIndex];
        CheckingAnswer();
        UpdateQuestUI();
    }
    
  

    public void OnNextButton()
    {
        index++;
        if (index > QuestManager.Instance.selectAnswer.Count)
        {
            index = QuestManager.Instance.selectAnswer.Count-1;
        }
        if (index >= QuestManager.Instance.selectAnswer.Count)
        {
            return;
        }
        else
        {
            SetQuestion();
            CheckingAnswer();
            UpdateQuestUI();
        }
    }
    public void OnBeforeButton()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        if (index < 0 )
        {
           return;
        } else
        {
            SetQuestion();
            CheckingAnswer();
            UpdateQuestUI();
        }
       
    }
    public void CheckingAnswer()
    {
       
        if (currentSelectAnswer == currentQuestion.correctAnswer)
        {
            Debug.Log("Dung");
        }
        else
        {
            Debug.Log("Sai");     
        }
    }

    public void SetQuestion()
    {
        currentSelectAnswer = QuestManager.Instance.selectAnswer[index];
        currentPassIndex = QuestManager.Instance.passIndex[index];
        currentQuestion = QuestManager.Instance.questions[currentPassIndex];
    }
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
            }
            else
            {
                answerList[i].transform.parent.gameObject.SetActive(true);
                //  Debug.Log("not nulll");
            }
        }


        for (int i = 0; i <answerList.Count; i++)
        {
            if (answerList[i].text == currentSelectAnswer)
            {
                answerList[i].color = Color.green;
            } else
            {
                answerList[i].color = Color.red;
            }
        }
      
       

    }
}
