using UnityEngine;
using TMPro;

public class QuestionCounter : MonoBehaviour
{
    public TextMeshProUGUI questionText;  
    private int currentQuestion = 1;  
    private int totalQuestions = 10; 

    void Start()
    {
        UpdateQuestionText();
    }

    public void NextQuestion()
    {
        if (currentQuestion < totalQuestions)
        {
            currentQuestion++;
            StartCoroutine(AnimateNumberChange(currentQuestion - 1, currentQuestion));
        }
    }

    private void UpdateQuestionText()
    {
        questionText.text = currentQuestion.ToString("00") + "/" + totalQuestions;
    }

    private System.Collections.IEnumerator AnimateNumberChange(int oldNumber, int newNumber)
    {
        float duration = 0.3f;  
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            int interpolatedValue = Mathf.FloorToInt(Mathf.Lerp(oldNumber, newNumber, t));
            questionText.text = interpolatedValue.ToString("00") + "/" + totalQuestions;
            yield return null;
        }
        questionText.text = newNumber.ToString("00") + "/" + totalQuestions;
    }
}
