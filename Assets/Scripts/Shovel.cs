using UnityEngine;
using UnityEngine.EventSystems;

public class Shovel : MonoBehaviour
{
    public void UseShovel()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if(results.Count > 1)
        {
            Debug.Log("Attempting to remove plant");
            foreach(var hit in results)
            {
                if(hit.gameObject == gameObject) continue;

                Soil temp = hit.gameObject.GetComponent<Soil>();
                if(temp != null && temp.GetPlant() != null)
                {
                    temp.RemovePlant();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Nothing to Remove");
        }
    }
}
