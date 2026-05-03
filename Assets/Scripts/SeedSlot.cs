using UnityEngine;
using UnityEngine.UI;

public class SeedSlot : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private GameObject _seedPrefab;
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

        GameObject seed = Instantiate(_seedPrefab, PlayerHand.Singleton.GetGameplayCanvas());
        PlayerHand.Singleton.Interact(seed, seed.GetComponent<Item>());

        // PlayerHand.Singleton.TryPickUp(_data, _visualPrefab);
    }
}
