using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Singleton { get; private set; }

    [Header("Setup")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private List<CustomerSO> customerPool;

    [Header("Spawning")]
    [SerializeField] private int maxCustomers = 3;
    [SerializeField] private float timeBetweenCustomers = 15f;

    [Header("Events")]
    public UnityEvent<Customer> onCustomerArrived;
    public UnityEvent<Customer> onCustomerServed;
    public UnityEvent<Customer> onCustomerLeft;

    private List<Customer> activeCustomers = new List<Customer>();
    private float spawnTimer = 0f;

    void Awake()
    {
        if (Singleton == null) Singleton = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        TrySpawnCustomer();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeBetweenCustomers)
        {
            spawnTimer = 0f;
            TrySpawnCustomer();
        }
    }

    public void TrySpawnCustomer()
    {
        if (activeCustomers.Count >= maxCustomers) return;
        if (customerPool == null || customerPool.Count == 0) return;

        CustomerSO data = customerPool[Random.Range(0, customerPool.Count)];
        SpawnCustomer(data);
    }

    public void SpawnCustomer(CustomerSO data)
    {
        Customer customer = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.identity, customerSpawnPoint);
        customer.Initialize(data);
        activeCustomers.Add(customer);
        onCustomerArrived?.Invoke(customer);
    }

    public void ServeCustomer(Customer customer)
    {
        if (!activeCustomers.Contains(customer)) return;

        activeCustomers.Remove(customer);
        onCustomerServed?.Invoke(customer);
        customer.Leave();
    }

    public void DismissCustomer(Customer customer)
    {
        if (!activeCustomers.Contains(customer)) return;

        activeCustomers.Remove(customer);
        onCustomerLeft?.Invoke(customer);
        customer.Leave();
    }

    public List<Customer> GetActiveCustomers() => activeCustomers;
}