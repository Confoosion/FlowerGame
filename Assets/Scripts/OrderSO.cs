using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOrder", menuName = "Scriptable Objects/Order")]
public class OrderSO : ScriptableObject
{
    [System.Serializable]
    public class OrderItem
    {
        public FlowerSO flower;
        public int quantity;
    }

    public List<OrderItem> items = new List<OrderItem>();
    public float timeLimit = 60f; // 0 = no time limit
}