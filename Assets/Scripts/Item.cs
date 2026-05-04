using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ItemData _data;
    [SerializeField] private LayerMask actionLayer;
    [SerializeField] private UnityEvent itemAction;
    [SerializeField] private UnityEvent stopAction;
    private GameObject _object;
    private bool isActionActive;
    private float actionInterval = 0.1f;
    private Coroutine actionRoutine = null;

    void Awake()
    {
        _object = this.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerHand.Singleton.Interact(_object, this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isActionActive = false;
        if(actionRoutine != null)
            StopCoroutine(actionRoutine);
    }

    public void UseItem()
    {
        if(HasAction())
        {
            isActionActive = true;
            actionRoutine = StartCoroutine(UsingItem());
        }
    }

    IEnumerator UsingItem()
    {
        Debug.Log("Using item");
        while(isActionActive)
        {
            itemAction.Invoke();
            Debug.Log("Using");
            yield return new WaitForSeconds(actionInterval);
        }

        if(stopAction.GetPersistentEventCount() > 0)
            stopAction.Invoke();
        
        yield return null;
    }

    public bool HasAction() => itemAction.GetPersistentEventCount() > 0;
    public bool CanUseAction()
    {
        var results = PlayerHand.Singleton.GetHandCast();

        foreach(var hit in results)
        {
            if((actionLayer.value & (1 << hit.gameObject.layer)) != 0)
            {
                return(true);
            }
        }

        return(false);
    }
    public ItemData GetData() => _data;
}
