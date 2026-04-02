using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Singleton { get; private set; }

    [Header("Order Settings")]
    [SerializeField] private List<CustomerSO> customerPool;
    [SerializeField] private List<FlowerSO> randomFlowerPool;
    [SerializeField] private int maxActiveOrders = 3;
    [SerializeField] private float timeBetweenOrders = 15f;

    [Header("Random Order Settings")]
    [SerializeField] private int minItems = 1;
    [SerializeField] private int maxItems = 3;
    [SerializeField] private int maxQuantity = 2;
    [SerializeField] private float orderTimeLimit = 60f;

    [Header("Events")]
    public UnityEvent<Order> onOrderAdded;
    public UnityEvent<Order> onOrderFulfilled;
    public UnityEvent<Order> onOrderExpired;

    private List<Order> activeOrders = new List<Order>();
    private float spawnTimer = 0f;

    void Awake()
    {
        if (Singleton == null) Singleton = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        TickOrders();
        TickSpawner();
    }

    private void TickOrders()
    {
        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            Order order = activeOrders[i];
            if (order.timeLimit <= 0f) continue; // no time limit

            order.timeRemaining -= Time.deltaTime;
            if (order.timeRemaining <= 0f)
            {
                activeOrders.RemoveAt(i);
                onOrderExpired?.Invoke(order);
            }
        }
    }

    private void TickSpawner()
    {
        if (activeOrders.Count >= maxActiveOrders) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeBetweenOrders)
        {
            spawnTimer = 0f;

            if(customerPool == null || customerPool.Count == 0) return;

            CustomerSO customer = customerPool[UnityEngine.Random.Range(0, customerPool.Count)];
            Order order = Order.FromRandomCustomerOrder(customer);
            if(order != null) AddOrder(order);
        }
    }

    public void SpawnRandomOrder(CustomerSO customer)
    {
        if (activeOrders.Count >= maxActiveOrders) return;

        Order order = Order.Random(customer, randomFlowerPool, minItems, maxItems, maxQuantity, orderTimeLimit);
        AddOrder(order);
    }

    public void SpawnPresetOrder(CustomerSO customer, OrderSO preset)
    {
        if (activeOrders.Count >= maxActiveOrders) return;

        Order order = Order.FromPreset(customer, preset);
        AddOrder(order);
    }

    public void AddOrder(Order order)
    {
        activeOrders.Add(order);
        onOrderAdded?.Invoke(order);
    }

    // Add this to OrderManager.cs
    public void RemoveOrder(Order order)
    {
        if (activeOrders.Contains(order))
            activeOrders.Remove(order);
    }

    // Call this when the player hands over a flower
    public bool TryServeFlower(FlowerSO flower)
    {
        foreach (Order order in activeOrders)
        {
            foreach (Order.OrderItem item in order.items)
            {
                if (item.flower == flower && !item.IsFulfilled)
                {
                    item.quantityFulfilled++;

                    if (order.IsFullyFulfilled)
                    {
                        activeOrders.Remove(order);
                        onOrderFulfilled?.Invoke(order);
                    }

                    return true;
                }
            }
        }

        Debug.Log($"{flower.name} is not needed for any active order.");
        return false;
    }

    public List<Order> GetActiveOrders() => activeOrders;

    public List<FlowerSO> GetFlowerPool() => randomFlowerPool;
}