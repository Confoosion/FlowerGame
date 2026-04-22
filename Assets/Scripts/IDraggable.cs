using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offsetPosition;
    private Image draggedImage;
    
    private string GAMEPLAY_NAME = "GAMEPLAY_CANVAS";
    private Transform GAMEPLAY;

    private SelectHighlight activeHighlight;

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

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if(results.Count > 0)
        {
            foreach(var hit in results)
            {
                SelectHighlight temp = hit.gameObject.GetComponent<SelectHighlight>();
                if(temp != null && temp != activeHighlight)
                {
                    if(activeHighlight != null)
                        activeHighlight.DisplayHighlight(false);
                    
                    activeHighlight = temp;
                    activeHighlight.DisplayHighlight(true);
                }
            }
        }
        else
        {
            RemoveHighlight();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedImage.raycastTarget = true;
        RemoveHighlight();
    }

    private void RemoveHighlight()
    {
        if(activeHighlight != null)
        {
            activeHighlight.DisplayHighlight(false);
            activeHighlight = null;
        }
    }
}
