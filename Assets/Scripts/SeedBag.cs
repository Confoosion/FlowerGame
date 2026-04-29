// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class SeedBag : MonoBehaviour, IBeginDragHandler
// {
//     [SerializeField] private SeedBagSO seedBagSO;
//     private Image seedBagImage;
    
//     private SeedSlot seedSlot;

//     void Awake()
//     {
//         seedBagImage = GetComponent<Image>();
//         seedBagImage.sprite = seedBagSO.seedSprite;

//         seedSlot = transform.parent.GetComponent<SeedSlot>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         if(seedSlot != null)
//         {
//             seedSlot.RefillSlot();
//             seedSlot = null;
//         }
//     }

//     public GameObject PlantSeed(Transform soil)
//     {
//         GameObject planted = Instantiate(seedBagSO.flowerObject, soil);
//         planted.transform.localPosition = new Vector2(0f, 0f);
//         Destroy(this.gameObject);
//         return(planted);
//     }

//     public SeedBagSO GetSeedBagSO()
//     {
//         return(seedBagSO);
//     }
// }
