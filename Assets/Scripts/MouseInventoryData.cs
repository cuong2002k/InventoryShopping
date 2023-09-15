using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MouseInventoryData : MonoBehaviour
{
    public Image spriteIcon;
    public Text ItemCount;
    public InventorySlot assignedInventorySlot;
    private void Start()
    {
        ClearMouseSlot();
    }

    public void ClearMouseSlot()
    {
        spriteIcon.color = Color.clear;
        ItemCount.text = "";
        assignedInventorySlot.item = null;
    }

    public void UpdateMouseSlot(InventorySlot inventorySlot)
    {
        assignedInventorySlot.AssignInventory(inventorySlot);
        this.spriteIcon.sprite = inventorySlot.item.icon;
        this.ItemCount.text = inventorySlot.stackSize.ToString();
        this.spriteIcon.color = Color.white;
    }

    private void Update()
    {
        if (assignedInventorySlot.item != null)
        {
            this.transform.position = Mouse.current.position.ReadValue();
            if (!IsPointerOverUIObject() && Mouse.current.leftButton.wasPressedThisFrame)
            {
                ClearMouseSlot();
                assignedInventorySlot.ClearSlot();

            }

        }
    }

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue(); ;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return (results.Count > 0);
    }
}
