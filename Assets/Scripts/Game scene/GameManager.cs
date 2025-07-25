using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<HeadgearSlot> allHeadgearSlots;
    public Button checkScoreButton;

    private void Awake()
    {
        Instance = this;
        checkScoreButton.gameObject.SetActive(false);
    }

    public void CheckAllMatched()
{
    if (allHeadgearSlots == null || allHeadgearSlots.Count == 0)
    {
        Debug.LogError("Headgear slots list is empty or null!");
        return;
    }

    foreach (var slot in allHeadgearSlots)
    {
        if (!slot.HasFact())
        {
            checkScoreButton.gameObject.SetActive(false);
            return;
        }
    }

    checkScoreButton.gameObject.SetActive(true);
}


    public void ResetGame()
    {
        foreach (var slot in allHeadgearSlots)
        {
            slot.ResetMatch();

            // Remove any child from DropZone
            Transform dropZone = slot.transform.Find("DropZone");
            if (dropZone.childCount > 0)
            {
                Transform fact = dropZone.GetChild(0);
                fact.SetParent(GameObject.Find("FactsPanel").transform); // back to fact panel
                fact.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }



        checkScoreButton.gameObject.SetActive(false);
    }
}






        public void SubmitResultsAndLoadScene()
        {
            GameResultData.Results.Clear();
            GameResultData.CorrectCount = 0;

            foreach (var slot in allHeadgearSlots)
            {
                string headgearName = slot.gameObject.name;
                string userAnswer = "";
                string correctAnswer = slot.expectedFactID;

                Transform dropZone = slot.transform.Find("DropZone");
                if (dropZone.childCount > 0)
                {
                    var factData = dropZone.GetChild(0).GetComponent<FactData>();
                    if (factData != null)
                    {
                        userAnswer = factData.factText;

                        GameResultData.Results.Add(new MatchResult
                        {
                            headgearName = headgearName,
                            userAnswer = userAnswer,
                            correctAnswer = correctAnswer,
                            isCorrect = factData.factID == correctAnswer
                        });

                        if (factData.factID == correctAnswer)
                            GameResultData.CorrectCount++;
                    }
                }
            }

            SceneManager.LoadScene("ResultScene");
        }




        public void GoToResultScene()
        {
            //GameResultData.Instance.matchResults.Clear();

            GameResultData.Instance.Results.Clear();
            GameResultData.Instance.CorrectCount = 0;


            foreach (var slot in allHeadgearSlots)
            {
                var factText = slot.transform.Find("DropZone").GetChild(0).GetComponent<FactData>().factText;
                var correctText = slot.transform.Find("DropZone").GetChild(0).GetComponent<FactData>().factText; // update if needed
                bool isCorrect = slot.IsCorrectMatch();

                GameResultData.Instance.matchResults.Add(new MatchResult
                {
                    headgearName = slot.gameObject.name,
                    headgearImage = slot.transform.Find("HeadgearImage").GetComponent<Image>().sprite,
                    userAnswer = factText,
                    correctAnswer = correctText,
                    isCorrect = isCorrect
                });
            }

            SceneManager.LoadScene("ResultScene");
        }




}