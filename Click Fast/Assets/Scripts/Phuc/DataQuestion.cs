using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DataQuestion : MonoBehaviour
{
    public static DataQuestion Instance { get; private set; }

    public List<Question> QuestionList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
