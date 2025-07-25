using UnityEngine;
using UnityEngine.UI;

public class HeadgearCard : MonoBehaviour
{
    public Image backgroundImage;

    public void HighlightCard()
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = new Color32(207, 236, 246, 255); // Light yellow
        }
    }

    public void ResetHighlight()
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = Color.white;
        }
    }
}
