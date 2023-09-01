using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots; // store list inventoryslot
    public List<InventorySlot> InventorySlots => inventorySlots; // get 
    public int InventorySize => inventorySlots.Count; // size

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)// constructor
    {
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(Item item, int amount) // add item to slot in inventory
    {
        if (ContainsItem(item, out List<InventorySlot> inventorySlot))// check if exits "item" to slot => add to stack of item
        {
            foreach (InventorySlot slot in inventorySlot)
            {
                if (slot.RoomLeftToStack(amount))
                {
                    slot.AddToStack(amount);
                    OnInventorySlotChanged?.Invoke(slot); // active event
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeToSlot))
        {
            freeToSlot.UpdateToInventorySlot(item, amount);
            OnInventorySlotChanged?.Invoke(freeToSlot); // active event
            return true;
        }


        return false;
    }

    public bool ContainsItem(Item item, out List<InventorySlot> inventorySlot) // check item exits in slot return slot exits
    {
        inventorySlot = inventorySlots.Where(i => i.item == item).ToList(); // select all slot exit item
        Debug.Log(inventorySlot.Count);
        return (inventorySlot.Count >= 1);
    }

    public bool HasFreeSlot(out InventorySlot inventorySlot) // check free slot not contains any item in slot
    {
        inventorySlot = inventorySlots.FirstOrDefault(i => i.item == null); // find free slot the first
        return (inventorySlot != null);
    }


}
