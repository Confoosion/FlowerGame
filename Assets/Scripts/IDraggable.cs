using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offsetPosition;
    private Image draggedImage;

    void Awake()
    {
        draggedImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offsetPosition = (Vector2)transform.position - eventData.position;
        draggedImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + offsetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedImage.raycastTarget = true;
    }
}
