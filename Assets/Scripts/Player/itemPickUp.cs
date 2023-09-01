using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class itemPickUp : MonoBehaviour
{
    public Item item;
    public float pickUpRaditus = 1f;

    private SphereCollider itemCollider;

    private void Start()
    {
        itemCollider = GetComponent<SphereCollider>();
        itemCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        InventoryHolder inventorySlot = other.GetComponent<InventoryHolder>();
        if (inventorySlot != null)
        {
            if (inventorySlot.InventorySystem.AddToInventory(item, 1))
            {
                Destroy(this.gameObject);
            }
        }
    }

}
