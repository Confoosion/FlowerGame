using UnityEngine;
using UnityEngine.EventSystems;

public class SeedSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject flowerObject;

    private GameObject spawnedObject;

    void Start()
    {
        RefillSlot();
    }

    void Update()
    {
        if(!spawnedObject.GetComponent<Flower>().IsJustSpawned())
        {
            RefillSlot();
        }
    }

    public void RefillSlot()
    {
        spawnedObject = Instantiate(flowerObject, transform.position, Quaternion.identity, transform);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if(dropped.GetComponent<Flower>())
        {
            if(dropped.GetComponent<Flower>().GetFlower() == flowerObject.GetComponent<Flower>().GetFlower())
            {
                Destroy(dropped);
            }
        }
    }
}
