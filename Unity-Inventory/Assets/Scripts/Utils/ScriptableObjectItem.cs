using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "ScriptableObjectItem", menuName = "Frank/Utils/ScriptableObjectItem" + "", order = 1)]
public class ScriptableObjectItem : ScriptableObject
{
    //https://docs.unity3d.com/Manual/class-ScriptableObject.html
    public string itemName;
    public Item.itemsType itemType;
    public Item.itemsSubType itemSubType;
    public int itemLevel;
    public int damage;
    public int defense;
    public float weight;
    public float durability;
    public int itemSlot;
    public Sprite itemSprite;
    public GameObject itemModel;
    //public Sprite imagen;
}
