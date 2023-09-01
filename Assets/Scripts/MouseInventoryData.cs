using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseInventoryData : MonoBehaviour
{
    public Image spriteIcon;
    public Text ItemCount;

    private void Start()
    {
        spriteIcon.color = Color.clear;
        ItemCount.text = "";
    }
}
