using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDesc;
    [Tooltip("0 - comida, 1 - água, 2 - tapete, 3 - casa")]
    public int itemTypeID;
    public int itemID;
    public int price;
    public Sprite spr;
    public Sprite emptySpr;
    public bool bought;

}
