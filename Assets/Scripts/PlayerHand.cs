using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand Singleton { get; private set; }

    public ItemData heldItem { get; private set; }
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
            heldVisual.transform.position = GetWorldMousePos();
        }   
    }

    public bool TryPickUp(ItemData item, GameObject visual)
    {
        if(heldItem != null)
            return(false);

        heldItem = item;
        heldVisual = Instantiate(visual);
        return(true);
    }

    public ItemData Release()
    {
        ItemData item = heldItem;
        Destroy(heldVisual);
        heldItem = null;
        heldVisual = null;
        return(item);
    }

    private Vector3 GetWorldMousePos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 10f;

        return(Camera.main.ScreenToWorldPoint(screenPos));
    }
}
