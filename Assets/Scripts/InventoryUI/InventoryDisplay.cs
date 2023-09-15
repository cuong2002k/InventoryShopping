using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseInventoryData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    public InventorySystem InventorySystem => inventorySystem;
    [SerializeField] protected Dictionary<InventorySlotUi, InventorySlot> slotDictionary;
    protected Dictionary<InventorySlotUi, InventorySlot> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventorySystem inventoryToDisplay);

    public virtual void Start()
    {

    }

    public virtual void UpdateSlot(InventorySlot slotToUpdate) // register event
    {
        foreach (var slots in slotDictionary)
        {
            if (slots.Value == slotToUpdate)
            {
                slots.Key.UpdateUISlot(slotToUpdate);
            }
        }
    }

    public void SlotClicked(InventorySlotUi clickedSlot)
    {
        bool isShiftPress = Keyboard.current.leftShiftKey.isPressed;

        //if slot exists item and slot in mouse obj no contains item
        //then update item of mouse and clear slot
        if (clickedSlot.AssignInventorySlot.item != null
        && mouseInventoryItem.assignedInventorySlot.item == null)
        {
            if (isShiftPress && clickedSlot.AssignInventorySlot.SplitStack(out InventorySlot splitStack))
            {
                mouseInventoryItem.UpdateMouseSlot(splitStack);
                clickedSlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignInventorySlot);
                clickedSlot.ClearUISlot();
                return;
            }
        }

        // if slot not exists item and mouse obj contain item 
        // then update item in slot and clear mouse
        if (clickedSlot.AssignInventorySlot.item == null &&
        mouseInventoryItem.assignedInventorySlot.item != null
        )
        {
            clickedSlot.AssignInventorySlot.AssignInventory(mouseInventoryItem.assignedInventorySlot);
            clickedSlot.UpdateUISlot();
            mouseInventoryItem.ClearMouseSlot();
        }


        //both slot have an item - device that to do ...
        if (clickedSlot.AssignInventorySlot.item != null &&
        mouseInventoryItem.assignedInventorySlot.item != null
        )
        {
            bool isSameSlotItem = clickedSlot.AssignInventorySlot.item == mouseInventoryItem.assignedInventorySlot.item;
            // slot item a == slot item b and stacksize + item.size <= maxstacksize =>  plus stack
            if (isSameSlotItem
            && clickedSlot.AssignInventorySlot.RoomLeftToStack(mouseInventoryItem.assignedInventorySlot.stackSize))
            {
                clickedSlot.AssignInventorySlot.AssignInventory(mouseInventoryItem.assignedInventorySlot);
                clickedSlot.UpdateUISlot();
                this.mouseInventoryItem.ClearMouseSlot();
            }
            // is same item but 
            else if (isSameSlotItem &&
            !clickedSlot.AssignInventorySlot.RoomLeftToStack(
                    mouseInventoryItem.assignedInventorySlot.stackSize,
                    out int leftToStack)
            )
            {
                if (leftToStack < 1) // if slot is full swap them
                {
                    this.SwapInventorySlotUI(clickedSlot);
                }
                else
                { // fill the remaining space (lap day khoang chong con lai)
                    int remainingOnMouse = mouseInventoryItem.assignedInventorySlot.stackSize - leftToStack;
                    clickedSlot.AssignInventorySlot.AddToStack(leftToStack);
                    clickedSlot.UpdateUISlot();

                    InventorySlot newInventorySlot = new InventorySlot(mouseInventoryItem.assignedInventorySlot.item, remainingOnMouse);
                    mouseInventoryItem.ClearMouseSlot();
                    mouseInventoryItem.UpdateMouseSlot(newInventorySlot);

                }
            }
            //slot item a != slot item b => swap them
            else if (!isSameSlotItem)
            {
                SwapInventorySlotUI(clickedSlot);
            }
        }


    }

    public void SwapInventorySlotUI(InventorySlotUi ClickedUISlot)
    {
        InventorySlot clonedSlot = new InventorySlot(
            mouseInventoryItem.assignedInventorySlot.item,
            mouseInventoryItem.assignedInventorySlot.stackSize);

        mouseInventoryItem.ClearMouseSlot();
        mouseInventoryItem.UpdateMouseSlot(ClickedUISlot.AssignInventorySlot);

        ClickedUISlot.ClearUISlot();
        ClickedUISlot.AssignInventorySlot.AssignInventory(clonedSlot);
        ClickedUISlot.UpdateUISlot();


    }

}
