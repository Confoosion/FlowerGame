using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoilPlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool isOccupied = false;

    [SerializeField] private Color wateredColor;
    [SerializeField] private Color unwateredColor;

    [SerializeField] private bool isWatered = false;
    [SerializeField] private float timeToWater = 2f;
    [SerializeField] private float waterLevel = 0f;
    [SerializeField] private float depletionScale = 0.1f;

    // [SerializeField] private Fertilizer currentFertlizer;
    private Image image;

    public static List<SoilPlot> allPlots = new List<SoilPlot>();

    void OnEnable() => allPlots.Add(this);
    void OnDisable() => allPlots.Remove(this);

    void Awake()
    {
        image = GetComponent<Image>();
        image.color = unwateredColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Something got dropped on me!");
        
        GameObject dropped = eventData.pointerDrag;
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
            waterLevel -= Time.deltaTime * depletionScale;
            if(waterLevel <= 0f)
            {
                isWatered = false;
                image.color = unwateredColor;
                waterLevel = 0f;
            }
        }
        // else if(isBeingWatered)
        // {
        //     timer += Time.deltaTime;
        //     if(timer >= timeToWater)
        //     {
        //         FinishedWateringSoil();
        //     }
        // }
    }

    public void WaterSoil(float scale = 1f)
    {
        waterLevel += Time.deltaTime * scale;
        if(waterLevel >= timeToWater)
        {
            waterLevel = timeToWater;
            FinishedWateringSoil();
        }
    }

    private void FinishedWateringSoil()
    {
        image.color = wateredColor;
        isWatered = true;
    }

    public bool IsWatered()
    {
        return(isWatered);
    }

    public float GetWaterTime()
    {
        return(timeToWater);
    }
}
