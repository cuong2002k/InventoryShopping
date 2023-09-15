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

    public void AssignInventory(InventorySlot invSlot)
    {
        if (invSlot.item == item) // check type item => + stack
        {
            this.AddToStack(invSlot.stackSize);

        }
        else
        {
            this.item = invSlot.item;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
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

    public bool RoomLeftToStack(int amountToAdd)
    {
        return (this.stackSize + amountToAdd <= item.maxStack);
    }

    public bool RoomLeftToStack(int amountToAdd, out int amountMaining)
    {
        amountMaining = item.maxStack - stackSize;
        return RoomLeftToStack(amountToAdd);
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(this.stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(this.stackSize/2);
        this.RemoveFromStack(halfStack);
        splitStack = new InventorySlot(this.item, halfStack);
        return true;

    }
}
