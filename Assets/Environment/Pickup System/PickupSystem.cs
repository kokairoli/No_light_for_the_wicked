using Inventory.Model;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.inventoryItem, item.quantity);
            if (reminder == 0)
            {
                item.DestroyItem();
            }
            else
            {
                item.quantity = reminder;
            }
        }
    }
}
