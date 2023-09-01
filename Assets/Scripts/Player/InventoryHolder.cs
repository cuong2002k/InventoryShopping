using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected InventorySystem inventorySystem;
    [SerializeField] private int inventorySize;
    public InventorySystem InventorySystem => inventorySystem;
    public static UnityAction<InventorySystem> onDynamicInventoryDisplayRequested;

    private void Awake() {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
