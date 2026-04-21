using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ServingArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<Flower> flowersOnTable = new List<Flower>();

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Something got dropped on me!");
        
        GameObject dropped = eventData.pointerDrag;
        Flower flower = dropped.GetComponent<Flower>();

        if(flower != null && flower.IsFullyGrown())
        {
            flowersOnTable.Add(flower);
            flower.PutOnTable(this);
        }
    }

    public void RemoveFlower(Flower flower)
    {
        flowersOnTable.Remove(flower);
    }

    public void ServeToCustomer()
    {
        foreach(Flower flower in flowersOnTable)
        {
            OrderManager.Singleton.TryServeFlower(flower.GetFlower());
        }
    }
}
