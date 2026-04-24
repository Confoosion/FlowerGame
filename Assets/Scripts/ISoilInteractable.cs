using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ISoilInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent interactableEvent;
    [SerializeField] private bool overrideHighlightLock;
    private bool isPickedUp = false;
    private RectTransform rectTransform;
    private Vector2 offsetPosition;
    private PointerEventData eventData;
    private SelectHighlight activeHighlight;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(isPickedUp)
        {
            eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();

            rectTransform.position = eventData.position + offsetPosition;

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if(results.Count > 1)
            {
                foreach(var hit in results)
                {
                    if(hit.gameObject == gameObject) continue;

                    SelectHighlight temp = hit.gameObject.GetComponent<SelectHighlight>();
                    if(temp != null && temp != activeHighlight)
                    {
                        if(activeHighlight != null)
                        {
                            RemoveHighlight();
                        }
                    
                        if(!temp.IsHighlightLocked() || overrideHighlightLock)
                        {
                            activeHighlight = temp;
                            activeHighlight.DisplayHighlight(true);
                        }
                    }
                }
            }
            else
            {
                RemoveHighlight();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Pick up Object
        if(!isPickedUp)
        {
            offsetPosition = (Vector2)transform.position - eventData.position;
            isPickedUp = true;
        }
        // Do action
        else if(activeHighlight != null)
        {
            Debug.Log("Doing action!");
            interactableEvent.Invoke();
        }
        // Drop Object
        else
        {
            isPickedUp = false;
        }
    }

    public void RemoveHighlight()
    {
        // Debug.Log("Removing highlight");
        if(activeHighlight != null)
        {
            activeHighlight.DisplayHighlight(false);
            activeHighlight = null;
        }
    }
}
