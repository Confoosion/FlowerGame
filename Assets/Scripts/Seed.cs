using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private GameObject flowerObj;
    private LayerMask soilLayer;

    void Awake()
    {
        soilLayer = LayerMask.GetMask("Soil");
    }

    public void PlaceSeed()
    {
        var results = PlayerHand.Singleton.GetHandCast();
        
        foreach(var hit in results)
        {
            if((soilLayer.value & (1 << hit.gameObject.layer)) != 0)
            {
                hit.gameObject.GetComponent<Soil>().SetSeed(flowerObj);
                break;
            }
        }

        Destroy(this.gameObject);
    }
}
