using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum weaponsType
    {
        none,
        melee,
        range,
        maxTypes
    }

    public enum armorsType
    {
        none,
        light,
        medium,
        heavy,
        maxTypes
    }

    public enum itemsSubType
    {
        none,
        weaponType,
        armorType,
        maxTypes
    }

    public string itemName;
    public itemsType itemType;

    public itemsSubType itemSubType;
    private weaponsType weaponType = weaponsType.none;
    private armorsType armorType = armorsType.none;

    public int itemLevel;
    public float weight;
    public float durability;
    public int itemSlot;
    public Sprite itemSprite;
    public GameObject itemModel;

    public void setWeaponType(weaponsType aType)
    {
        weaponType = aType;
    }

    public weaponsType getWeaponType()
    {
        return weaponType;
    }

    public void setArmorType(armorsType aType)
    {
        armorType = aType;
    }

    public armorsType getArmorType()
    {
        return armorType;
    }
}
