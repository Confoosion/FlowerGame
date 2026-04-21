using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offsetPosition;
    private Image draggedImage;
    
    private string GAMEPLAY_NAME = "GAMEPLAY_CANVAS";
    private Transform GAMEPLAY;

    void Awake()
    {
        draggedImage = GetComponent<Image>();

        GAMEPLAY = GameObject.Find(GAMEPLAY_NAME).transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offsetPosition = (Vector2)transform.position - eventData.position;
        draggedImage.raycastTarget = false;
        transform.SetParent(GAMEPLAY);
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
