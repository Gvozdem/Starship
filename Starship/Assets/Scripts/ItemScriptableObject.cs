using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { Default, Module }
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite icon;
}
