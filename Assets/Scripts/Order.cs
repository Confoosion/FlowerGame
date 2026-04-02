using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public CustomerSO customer;
    public float timeLimit;
    public float timeRemaining;

    public class OrderItem
    {
        public FlowerSO flower;
        public int quantity;
        public int quantityFulfilled;

        public bool IsFulfilled => quantityFulfilled >= quantity;
    }

    public List<OrderItem> items = new List<OrderItem>();
    public bool IsFullyFulfilled => items.TrueForAll(i => i.IsFulfilled);

    // Build from a specific customer + order
    public static Order FromPreset(CustomerSO customer, OrderSO preset)
    {
        Order order = new Order();
        order.customer = customer;
        order.timeLimit = preset.timeLimit;
        order.timeRemaining = preset.timeLimit;

        foreach (var item in preset.items)
        {
            order.items.Add(new OrderItem
            {
                flower = item.flower,
                quantity = item.quantity
            });
        }

        return order;
    }

    // Pick a random order from the customer's possible orders
    public static Order FromRandomCustomerOrder(CustomerSO customer)
    {
        if (customer.possibleOrders == null || customer.possibleOrders.Count == 0)
        {
            Debug.LogWarning($"{customer.customerName} has no possible orders assigned.");
            return Random(customer, OrderManager.Singleton.GetFlowerPool());
        }

        OrderSO preset = customer.possibleOrders[UnityEngine.Random.Range(0, customer.possibleOrders.Count)];
        return FromPreset(customer, preset);
    }

    // Fully random order from a flower pool, still tied to a customer
    public static Order Random(CustomerSO customer, List<FlowerSO> flowerPool,
                                int minItems = 1, int maxItems = 3,
                                int maxQuantity = 2, float timeLimit = 60f)
    {
        Order order = new Order();
        order.customer = customer;
        order.timeLimit = timeLimit;
        order.timeRemaining = timeLimit;

        int itemCount = UnityEngine.Random.Range(minItems, maxItems + 1);
        List<FlowerSO> pool = new List<FlowerSO>(flowerPool);

        for (int i = 0; i < itemCount && pool.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            FlowerSO chosen = pool[index];
            pool.RemoveAt(index);

            order.items.Add(new OrderItem
            {
                flower = chosen,
                quantity = UnityEngine.Random.Range(1, maxQuantity + 1)
            });
        }

        return order;
    }
}