using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand Singleton { get; private set; }

    public Item heldItem { get; private set; }
    [SerializeField] private Transform gameplayCanvas;
    private GameObject heldVisual;

    private PointerEventData eventData;
    private List<RaycastResult> results;

    void Awake()
    {
        if(Singleton == null)
            Singleton = this;
    }

    void Update()
    {
        if(heldVisual != null)
        {
            heldVisual.transform.position = Input.mousePosition;
        }

        eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();

        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
    }

    public void Interact(GameObject obj, Item item)
    {
        if(heldItem != null)
        {
            if(item.HasAction() && item.CanUseAction())
                item.UseItem();
            else
                DropItem(obj, item);
        }
        else
        {
            PickUpItem(obj, item);
        }
    }

    private void PickUpItem(GameObject obj, Item item)
    {
        heldVisual = obj;
        heldItem = item;
    }

    private void DropItem(GameObject obj, Item item)
    {
        heldVisual = null;
        heldItem = null;
    }

    public Transform GetGameplayCanvas() => gameplayCanvas;
    public List<RaycastResult> GetHandCast() => results;
}
