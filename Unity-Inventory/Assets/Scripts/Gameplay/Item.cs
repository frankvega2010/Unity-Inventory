using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum itemsType
    {
        none,
        armor,
        weapon,
        consumable,
        maxTypes
    }

    public enum itemsSubType
    {
        none,
        weaponMelee,
        weaponRange,
        armorLight,
        armorMedium,
        armorHeavy,
        maxTypes
    }

    public string itemName;
    public itemsType itemType;
    public itemsSubType itemSubType;
    public int itemLevel;
    public int damage;
    public int defense;
    public float weight;
    public float durability;
    public int itemSlot;
    public Sprite itemSprite;
    public GameObject itemModel;

    //public void setWeaponType(weaponsType aType)
    //{
    //    weaponType = aType;
    //}

    //public weaponsType getWeaponType()
    //{
    //    return weaponType;
    //}

    //public void setArmorType(armorsType aType)
    //{
    //    armorType = aType;
    //}

    //public armorsType getArmorType()
    //{
    //    return armorType;
    //}
}
