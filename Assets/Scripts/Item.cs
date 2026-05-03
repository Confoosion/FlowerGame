using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ItemData _data;
    [SerializeField] private UnityEvent itemAction;
    private GameObject _object;

    void Awake()
    {
        _object = this.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerHand.Singleton.Interact(_object, this);
    }

    public void UseItem()
    {
        if(HasAction())
        {
            itemAction.Invoke();
        }
    }

    public bool HasAction() => itemAction.GetPersistentEventCount() > 0;
    public bool CanUseAction()
    {
        return(false);
    }
    public ItemData GetData() => _data;
}
