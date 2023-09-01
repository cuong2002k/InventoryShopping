using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class InventorySlot
{
    public Item item; // item of inventory
    public int stackSize; // size of item

    public InventorySlot(Item item, int stackSize) // constructor create inventoryslot
    {
        UpdateToInventorySlot(item, stackSize);
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        this.item = null;
        this.stackSize = -1;
    }

    public void UpdateToInventorySlot(Item item, int stackSize)
    {
        this.item = item;
        this.stackSize = stackSize;
    }

    public void AddToStack(int stackSize) // add amount item
    {
        this.stackSize += stackSize;
    }

    public void RemoveFromStack(int stackSize)
    {
        this.stackSize -= stackSize;
    }

    public bool RoomLeftToStack(int stackSize)
    {
        return (this.stackSize + stackSize <= item.maxStack);
    }

    public bool RoomLeftToStack(int stackSize, out int amountMaining)
    {
        amountMaining = item.maxStack - stackSize;
        return RoomLeftToStack(amountMaining);
    }
}
