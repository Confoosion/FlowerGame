using UnityEngine;
using UnityEngine.UI;

public class SeedSlot : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private GameObject _visualPrefab;
    // [SerializeField] private GameObject flowerObject;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        if(PlayerHand.Singleton.heldItem != null)
            return;

        PlayerHand.Singleton.TryPickUp(_data, _visualPrefab);
    }
}
