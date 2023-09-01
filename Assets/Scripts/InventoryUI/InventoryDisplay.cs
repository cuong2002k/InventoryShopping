using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseInventoryData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    public InventorySystem InventorySystem => inventorySystem;
    protected Dictionary<InventorySlotUi, InventorySlot> slotDictionary;
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

    public void ClickedSlot(InventorySlotUi slot)
    {
        Debug.Log(slot.gameObject.name);
    }

}
