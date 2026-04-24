using UnityEngine;
using UnityEngine.EventSystems;
public class Soil : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject plant;
    [SerializeField] private bool isWatered = false;
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

    public GameObject GetPlant()
    {
        return(plant);
    }

    public void RemovePlant()
    {
        Destroy(plant);
        plant = null;

        Debug.Log("Removed plant");
    }

    public void WaterSoil()
    {
        isWatered = true;
    }
}
