using UnityEngine;
using UnityEngine.EventSystems;
public class Soil : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject plant;
    private SelectHighlight selectHighlight;

    void Awake()
    {
        selectHighlight = GetComponent<SelectHighlight>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        SeedBag droppedSeed = dropped.GetComponent<SeedBag>();
        if(droppedSeed != null && plant == null)
        {
            dropped.GetComponent<IDraggable>().RemoveHighlight();
            plant = droppedSeed.PlantSeed(transform);
            selectHighlight.LockHighlight(true);
        }
    }
}
