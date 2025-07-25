using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class DropZone : MonoBehaviour, IDropHandler
{
    private HeadgearSlot headgearSlot;

    private void Awake()
    {
        headgearSlot = GetComponentInParent<HeadgearSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableFact droppedFact = eventData.pointerDrag.GetComponent<DraggableFact>();

        if (droppedFact != null && transform.childCount == 0)
        {
            droppedFact.transform.SetParent(transform);
            RectTransform droppedRect = droppedFact.GetComponent<RectTransform>();
            droppedRect.anchoredPosition = Vector2.zero;

            // Resize to fit the DropZone fully
            droppedRect.offsetMin = Vector2.zero;
            droppedRect.offsetMax = Vector2.zero;
            droppedRect.anchorMin = new Vector2(0, 0);
            droppedRect.anchorMax = new Vector2(1, 1);



            string factText = droppedFact.GetComponentInChildren<TextMeshProUGUI>().text;
            headgearSlot.SetMatchedFact(factText);

            GameManager.Instance.CheckAllMatched(); // next step: GameManager
        }

        // Change HeadgearCard background color after fact is dropped
                HeadgearCard card = GetComponentInParent<HeadgearCard>();
            if (card != null)
            {
                card.HighlightCard();
            }

    }
}
