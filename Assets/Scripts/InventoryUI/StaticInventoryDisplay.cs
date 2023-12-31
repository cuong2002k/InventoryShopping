using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlotUi[] inventoryUIContainer;
    public override void Start()
    {
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
            AssignSlot(inventorySystem);
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlotUi, InventorySlot>();
        if (inventorySystem.InventorySize != inventoryUIContainer.Length)
        {
            Debug.Log($"The Length out size {this.gameObject}");
        }
        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            SlotDictionary.Add(inventoryUIContainer[i], inventoryToDisplay.InventorySlots[i]);
            inventoryUIContainer[i].InitUISlot(inventoryToDisplay.InventorySlots[i]);
        }


    }
}
