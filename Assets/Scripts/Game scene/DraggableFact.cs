// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

// public class DraggableFact : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     public Transform originalParent;
//     private CanvasGroup canvasGroup;
//     private RectTransform rectTransform;
//     private Vector3 originalPosition;
//     public GameObject greyOverlay; 

//     void Start()
//     {
//         originalParent = transform.parent;
//         originalPosition = transform.localPosition;
//     }


//     private void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvasGroup = GetComponent<CanvasGroup>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         originalParent = transform.parent;
//         transform.SetParent(transform.root);  // bring to top for visibility
//         canvasGroup.blocksRaycasts = false;
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         canvasGroup.blocksRaycasts = true;

//         //  base.OnEndDrag(eventData);

//        // If not dropped in valid DropZone, snap back
//         if (transform.parent == originalParent || transform.parent.GetComponent<DropZone>() == null)
//         {
//             transform.SetParent(originalParent);
//             transform.localPosition = originalPosition;
//         }


//         //grey overlay

//         if (transform.parent.GetComponent<DropZone>() != null)
//      {
//             if (greyOverlay != null)
//             {
//                 greyOverlay.SetActive(true); // Show grey version
//             }

//             gameObject.SetActive(false); // Hide the draggable one
//     }

//     }
// }



using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableFact : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    public GameObject greyOverlay;

    private bool isDropped = false; //  Track if it has been successfully dropped

    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        if (greyOverlay != null)
            greyOverlay.SetActive(false); //  Ensure overlay starts hidden
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped) return; //  Block further dragging after dropped

        originalParent = transform.parent;
        transform.SetParent(transform.root);  // Bring to top
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped) return; //  Block further dragging after dropped

        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDropped) return; //  Block further dragging after dropped

        canvasGroup.blocksRaycasts = true;

        if (transform.parent == originalParent || transform.parent.GetComponent<DropZone>() == null)
        {
            // Invalid drop: return to original
            transform.SetParent(originalParent);
            transform.localPosition = originalPosition;
        }
        else
        {
            //  Valid drop:
            isDropped = true;

            // Lock position
            transform.SetParent(transform.parent); // Keep in DropZone
            transform.localPosition = Vector3.zero;

            // Activate grey overlay
            if (greyOverlay != null)
                greyOverlay.SetActive(true);
        }
    }
}
