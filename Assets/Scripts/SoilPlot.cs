using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoilPlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool isOccupied = false;

    [SerializeField] private Color wateredColor;
    [SerializeField] private Color unwateredColor;

    [SerializeField] private bool isWatered = false;
    [SerializeField] private float waterDuration = 10f;
    private float timer = 0f;
    // [SerializeField] private Fertilizer currentFertlizer;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        image.color = unwateredColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Something got dropped on me!");
        
        GameObject dropped = eventData.pointerDrag;
        // IDraggable draggable = dropped.GetComponent<IDraggable>();
        Flower flower = dropped.GetComponent<Flower>();
        if(flower != null && !isOccupied)
        {
            flower.PlantSeed(this);
            isOccupied = true;
        }
    }

    public void Vacate()
    {
        isOccupied = false;
    }

    void Update()
    {
        if(isWatered)
        {
            timer += Time.deltaTime;
            if(timer >= waterDuration)
            {
                isWatered = false;
                image.color = unwateredColor;
            }
        }
    }

    public void WaterSoil()
    {
        image.color = wateredColor;
        isWatered = true;
        timer = 0f;
    }
}
