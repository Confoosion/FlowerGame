using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Flower : MonoBehaviour, IBeginDragHandler
// IDragHandler, IEndDragHandler
{
    [SerializeField] private Image flowerImage;
    [SerializeField] private bool isPlanted;
    [SerializeField] private FlowerSO flower;

    private int currentStageIndex = 0;
    [SerializeField] private FlowerSO.FlowerStage currentStage;

    [SerializeField] private float growthTimer = 0f;

    private Vector2 offsetPosition;
    private SoilPlot currentPlot;

    private bool justSpawned = true;
    private bool finishedGrowing;

    void Start()
    {
        flowerImage.sprite = flower.seedSprite;
        currentStage = flower.stages[currentStageIndex];
    }

    void Update()
    {
        if(isPlanted && !finishedGrowing)
        {
            if(currentPlot.IsWatered())
            {
                growthTimer += Time.deltaTime;

                if(growthTimer >= currentStage.timeToGrow)
                {
                    NextStage();
                }
            }
        }
    }

    public void PlantSeed(SoilPlot plot)
    {
        if(!finishedGrowing)
        {
            isPlanted = true;
            currentPlot = plot;
            UpdateImage(currentStage);
        }
    }

    public void NextStage()
    {
        if(currentStageIndex < flower.stages.Count - 1)
        {
            currentStageIndex++;
            currentStage = flower.stages[currentStageIndex];
            UpdateImage(currentStage);

            growthTimer = 0f;
            Debug.Log("Advanced to stage " + currentStageIndex);
        }
        else
        {
            FinishGrowing();
        }
    }

    private void UpdateImage(FlowerSO.FlowerStage stage)
    {
        flowerImage.sprite = stage.stageSprite;
        flowerImage.SetNativeSize();
    }

    private void FinishGrowing()
    {
        finishedGrowing = true;

        flowerImage.sprite = flower.finishedSprite;
        flowerImage.SetNativeSize();

        Debug.Log("Flower is fully grown!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(finishedGrowing && currentPlot != null)
        {
            currentPlot.Vacate();
            currentPlot = null;
        }

        justSpawned = false;

        // offsetPosition = (Vector2)transform.position - eventData.position;
        // flowerImage.raycastTarget = false;
    }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     transform.position = eventData.position + offsetPosition;
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     flowerImage.raycastTarget = true;
    // }

    public FlowerSO GetFlower()
    {
        return(flower);
    }

    public bool IsJustSpawned()
    {
        return(justSpawned);
    }
}
