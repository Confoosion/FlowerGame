using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WateringCan : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image wateringCanImage;
    [SerializeField] private Sprite fullCan;
    [SerializeField] private Sprite emptyCan;
    [SerializeField] private RectTransform spoutTip;
    [SerializeField] private float WATER_CAPACITY = 10f;
    [SerializeField] private float waterAmount = 10f;
    
    private float pourAngle = 55f;
    private Canvas canvas;
    private SoilPlot hoveredPlot = null;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if(hoveredPlot != null && CheckWater())
        {
            PourWater(hoveredPlot);
            waterAmount -= Time.deltaTime;
        }
        else if(hoveredPlot == null)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        hoveredPlot = null;

        Vector2 spoutScreenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, spoutTip.position);

        foreach(SoilPlot plot in SoilPlot.allPlots)
        {
            RectTransform plotRect = plot.GetComponent<RectTransform>();
            if(RectTransformUtility.RectangleContainsScreenPoint(plotRect, spoutScreenPos, canvas.worldCamera))
            {
                hoveredPlot = plot;
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        hoveredPlot = null;
        transform.rotation = Quaternion.identity;
        waterAmount = WATER_CAPACITY;
        CheckWater();
    }

    private void PourWater(SoilPlot plot)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, pourAngle);
        plot.WaterSoil();
    }

    private bool CheckWater()
    {
        if(waterAmount <= 0f)
        {
            wateringCanImage.sprite = emptyCan;
            return(false);
        }

        wateringCanImage.sprite = fullCan;
        return(true);
    }
}
