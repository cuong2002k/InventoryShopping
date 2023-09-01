using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InventoryITem/Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon; // sprite of item
    public string displayName; // display name item
    public int maxStack; // max stack item
    [TextArea(4,4)]
    public string description; // description item example: when, where, using ?

    

}
