using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int slotsSize = 32;
    public bool[] slots = new bool[32];
    
    public int slotIndex;
    public List<Item> items;
    public Item item;
    public ScriptableObjectItem uniqueItem;
    // TO DO ... Add Inventory list (to be able to save and load up between sessions)

    public string[] weaponNames = new string[10];
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            addWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            addUniqueItem();
        }
    }

    public bool addSlot()
    {
        for (int i = 0; i < slotsSize; i++)
        {
            if(!slots[i])
            {
                slots[i] = true;
                slotIndex = i;
                return true;
            }
        }

        return false;
    }

    public void addWeapon()
    {
        item = new Item();
        addSlot();

        item.itemName = weaponNames[Random.Range(0,11)];
        item.itemType = Item.itemsType.weapon;
        item.itemSubType = (Item.itemsSubType)Random.Range(1, 3);
        item.itemLevel = 1;
        item.damage = Random.Range(30, 91);
        item.defense = 0;
        item.weight = Random.Range(5.0f, 9.0f);
        item.durability = Random.Range(25.0f, 100.0f);
        item.itemSlot = slotIndex; // find closest open slot on inventory.

        items.Add(item);
    }

    public void addUniqueItem()
    {
        item = new Item();
        addSlot();

        item.itemName = uniqueItem.itemName;
        item.itemType = uniqueItem.itemType;
        item.itemSubType = uniqueItem.itemSubType;
        item.itemLevel = uniqueItem.itemLevel;
        item.damage = uniqueItem.damage;
        item.defense = uniqueItem.defense;
        item.weight = uniqueItem.weight;
        item.durability = uniqueItem.durability;
        item.itemSlot = slotIndex; // find closest open slot on inventory.

        items.Add(item);
    }

    public void addSword() // TEST
    {
        item = new Item();
        addSlot();

        item.itemName = "Sword Test";
        item.itemType = Item.itemsType.weapon;
        item.itemSubType = Item.itemsSubType.weaponMelee;
        //item.setWeaponType(Item.weaponsType.melee);
        item.itemLevel = 1;
        item.damage = 30;
        item.defense = 0;
        item.weight = 13.2f;
        item.durability = 70.5f;  
        item.itemSlot = slotIndex; // find closest open slot on inventory.

        items.Add(item);
    }
}
