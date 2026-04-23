using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] private Image customerImage;
    [SerializeField] private TextMeshProUGUI customerNameText;
    [SerializeField] private Transform orderBubble;

    [SerializeField] private GameObject orderPrefab;

    private CustomerSO customerData;
    private Order currentOrder;

    public Order CurrentOrder => currentOrder;
    public CustomerSO CustomerData => customerData;

    public void Initialize(CustomerSO data)
    {
        customerData = data;
        customerImage.sprite = data.customerSprite;
        customerNameText.text = data.customerName;

        currentOrder = Order.FromRandomCustomerOrder(data);
        if (currentOrder != null)
        {
            OrderManager.Singleton.AddOrder(currentOrder);
            DisplayOrder();
        }
    }

    public void Leave()
    {
        if (currentOrder != null)
            OrderManager.Singleton.RemoveOrder(currentOrder);

        Destroy(gameObject);
    }

    public void Serve()
    {
        CustomerManager.Singleton.ServeCustomer(this);
    }

    private void DisplayOrder()
    {
        foreach(Order.OrderItem item in currentOrder.items)
        {
            // GameObject _item = Instantiate(orderPrefab, orderBubble);
            // _item.GetComponent<Image>().sprite = item.flower.noStemSprite;
        }
    }
}