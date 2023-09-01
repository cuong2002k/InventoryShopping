using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUi : MonoBehaviour
{
    [Header("Only UI")]
    [SerializeField] private Image inventoryIcon;
    [SerializeField] private Text itemCount;
    private Button button;

    [Header("Script Reference")]
    [SerializeField] private InventorySlot assignInventorySlot; // inventory slot in array
    public InventorySlot AssignInventorySlot => assignInventorySlot;

    public InventoryDisplay parrentDisplay { get; private set; }


    private void Awake()
    {
        ClearUISlot();
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        parrentDisplay = this.transform.parent.GetComponent<InventoryDisplay>();
    }

    public void InitUISlot(InventorySlot slot) // init all from inventory slot
    {
        assignInventorySlot = slot;
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot != null)
        {
            inventoryIcon.sprite = slot.item.icon;
            inventoryIcon.color = Color.white;
            if (slot.stackSize > 1)
            {
                itemCount.text = slot.stackSize.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }
        else
        {
            ClearUISlot();
        }
    }

    public void UpdateUISlot()
    {
        if (assignInventorySlot != null) UpdateUISlot(assignInventorySlot);
    }

    private void ClearUISlot()
    {
        assignInventorySlot?.ClearSlot();
        inventoryIcon.sprite = null;
        inventoryIcon.color = Color.clear;
        itemCount.text = "";
    }

    // register event button click
    private void OnUISlotClick()
    {
        this.parrentDisplay?.ClickedSlot(this);
    }


}
