using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GardenShears : MonoBehaviour, IDragHandler
{
    private Canvas canvas;
    private GraphicRaycaster raycaster;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach(RaycastResult result in results)
        {
            Flower flower = result.gameObject.GetComponent<Flower>();
            if(flower != null)
            {
                flower.ShearFlower();
            }
        }
    }
}
