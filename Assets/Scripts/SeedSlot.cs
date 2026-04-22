using UnityEngine;
using UnityEngine.EventSystems;

public class SeedSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject seedObject;
    // [SerializeField] private GameObject flowerObject;

    private GameObject spawnedObject;

    void Start()
    {
        RefillSlot();
    }

    // void Update()
    // {
    //     if(spawnedObject.GetComponent<IDraggable>().IsDragging())
    //     {
    //         RefillSlot();
    //     }
    // }

    public void RefillSlot()
    {
        spawnedObject = Instantiate(seedObject, transform.position, Quaternion.identity, transform);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        SeedBag seed = dropped.GetComponent<SeedBag>();
        if(seed != null && seed.GetSeedBagSO() == seedObject.GetComponent<SeedBag>().GetSeedBagSO())
        {
            Destroy(dropped);
        }
    }
}
