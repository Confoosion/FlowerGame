using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand Singleton { get; private set; }

    public ItemData heldItem { get; private set; }
    [SerializeField] private Transform gameplayCanvas;
    private GameObject heldVisual;

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
        heldItem = item.GetData();
    }

    private void DropItem(GameObject obj, Item item)
    {
        heldVisual = null;
        heldItem = null;
    }

    public Transform GetGameplayCanvas() => gameplayCanvas;
}
