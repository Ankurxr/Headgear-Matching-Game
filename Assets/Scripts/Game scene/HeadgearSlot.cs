using UnityEngine;

public class HeadgearSlot : MonoBehaviour
{
    public string expectedFactID;  // The correct ID to match
    private string matchedFactID;
    


    public void SetMatchedFact(string factID)
    {
        matchedFactID = factID;
    }

    public bool IsCorrectMatch()
    {
        return matchedFactID == expectedFactID;
    }

    public bool HasFact()
    {
        return !string.IsNullOrEmpty(matchedFactID);
    }

    public void ResetMatch()
    {
        matchedFactID = null;
    }
}
