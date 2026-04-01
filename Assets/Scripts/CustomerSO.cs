using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "Scriptable Objects/Customer")]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public Sprite customerSprite;

    [TextArea] public string description; // optional flavour text

    public List<OrderSO> possibleOrders; // orders this customer can request
}