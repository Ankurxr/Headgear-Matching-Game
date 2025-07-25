using System.Collections.Generic;
using UnityEngine;

public class GameResultData : MonoBehaviour
{
    public static GameResultData Instance;

    public List<MatchResult> Results = new List<MatchResult>();
    public int CorrectCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class MatchResult
{
    public string headgearName;
    public Sprite headgearImage;
    public string userAnswer;
    public string correctAnswer;
    public bool isCorrect;
}
