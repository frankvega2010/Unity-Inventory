using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public enum InventoryType
    {
        backpack,
        equipment,
        maxTypes
    }

    [Header("Slot Management")]
    public int slotsSize;
    public int maxBackpackSlots;
    public bool[] slots;
    public int slotIndex;

    [Header("Items Management")]
    public List<Item> items;
    public Item item;
    public int newIndex;
    public ScriptableObjectItem uniqueItem;
    // TO DO ... Add Inventory list (to be able to save and load up between sessions)

    
    [Header("Items Names")]
    public string[] weaponNames = new string[10];
    public string[] armorNames = new string[10];

    [Header("Items Sprites")]
    public int maxSprites;
    public int maxArmorSprites;
    
    public Sprite[] weaponSprites;
    public Sprite[] armorSprites;

    [Header("Unique Items")]
    public ScriptableObjectItem[] uniqueItems;

    [Header("Game Objects")]
    public GameObject inventoryUIGameObject;
    private InventoryUI inventoryUI;

    public GameObject equipmentInventoryUIGameObject;
    private InventoryUI equipmentInventoryUI;

    private void Start()
    {
        inventoryUI = inventoryUIGameObject.GetComponent<InventoryUI>();
        inventoryUI.InventoryDragItem = UpdateIndex;

        equipmentInventoryUI = equipmentInventoryUIGameObject.GetComponent<InventoryUI>();
        equipmentInventoryUI.InventoryDragItem = UpdateIndex;
    }

    public bool addSlot(int index)
    {
        if(index == -1)
        {
            for (int i = 0; i < slotsSize - (slotsSize - maxBackpackSlots) + 1; i++)
            {
                if (!slots[i])
                {
                    slots[i] = true;
                    slotIndex = i;
                    return true;
                }
            }
        }
        else
        {
            if (!slots[index])
            {

                if (index > maxBackpackSlots)
                {
                    Debug.Log("item is equipped");
                }
                slots[index] = true;
                slotIndex = index;
                return true;
            }
        }
        return false;
    }

    public bool addItem(int index,string name, bool isNewItem, Item.itemsType itemType) // make AddWeapon and AddArmor into AddItem.
    {
        if (isNewItem)
        {
            item = new Item();
        }

        if (addSlot(index))
        {
            if (isNewItem)
            {
                if (name == "")
                {
                    switch (itemType)
                    {
                        case Item.itemsType.armor:
                            addRandomArmor();
                            break;
                        case Item.itemsType.weapon:
                            addRandomWeapon();
                            break;
                        case Item.itemsType.consumable:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    addUniqueItem(name);
                }
            }


            switch (index)
            {
                case 28:
                    if (item.itemType != Item.itemsType.weapon)
                    {
                        slots[index] = false;
                        slotIndex = 0;
                        return false;
                    }
                    break;
                case 29:
                    if (item.itemType != Item.itemsType.armor)
                    {
                        slots[index] = false;
                        slotIndex = 0;
                        return false;
                    }
                    break;
            }

            item.itemSlot = slotIndex; // Find specific index set by player

            item.itemToDelete = inventoryUI.addItem(item.itemSprite, item.itemSlot);

            items.Add(item);

            return true;
        }
        else
        {
            Debug.Log("Couldnt add item.");
            return false;
        }
    }

    public void deleteItem(int index)
    {

                if (inventoryUI.panelGroup[index].GetComponent<Image>().sprite != inventoryUI.oldSprite)
                {
                    inventoryUI.removeItem(index);

                    foreach (Item newItem in items)
                    {
                        if (item.itemSlot == index)
                        {
                            items.Remove(newItem);
                            slots[index] = false;

                            if (index > maxBackpackSlots)
                            {
                                Debug.Log("item is no longer equipped");
                            }
                            break;
                        }
                    }

                }
                else
                {
                    Debug.Log("No item detected");
                }
    }

    public void deleteItem(int index, Item itemToRemove)
    {

                if (inventoryUI.panelGroup[index].GetComponent<Image>().sprite != inventoryUI.oldSprite)
                {
                    Debug.Log("Deleted item on slot number: " + index);
                    inventoryUI.removeItem(index);

                    items.Remove(itemToRemove);
                    slots[index] = false;

                    if (index > maxBackpackSlots)
                    {
                        Debug.Log("item is no longer equipped");
                    }

                }
                else
                {
                    Debug.Log("No item detected");
                }
    }

    private void addUniqueItem()
    {
        item.itemName = uniqueItem.itemName;
        item.itemType = uniqueItem.itemType;
        item.itemSubType = uniqueItem.itemSubType;
        item.itemLevel = uniqueItem.itemLevel;
        item.damage = uniqueItem.damage;
        item.defense = uniqueItem.defense;
        item.weight = uniqueItem.weight;
        item.durability = uniqueItem.durability;
        item.itemSprite = uniqueItem.itemSprite;
    }

    private void addUniqueItem(string name)
    {
        for (int i = 0; i < uniqueItems.Length; i++)
        {
            if(uniqueItems[i].itemName == name)
            {
                item.itemName = uniqueItems[i].itemName;
                item.itemType = uniqueItems[i].itemType;
                item.itemSubType = uniqueItems[i].itemSubType;
                item.itemLevel = uniqueItems[i].itemLevel;
                item.damage = uniqueItems[i].damage;
                item.defense = uniqueItems[i].defense;
                item.weight = uniqueItems[i].weight;
                item.durability = uniqueItems[i].durability;
                item.itemSprite = uniqueItems[i].itemSprite;
            }
        }
    }

    private void addRandomWeapon()
    {
        item.itemName = weaponNames[Random.Range(0, 10)];
        item.itemType = Item.itemsType.weapon;
        item.itemSubType = (Item.itemsSubType)Random.Range(1, 3);
        item.itemLevel = Random.Range(1, 101);
        item.damage = Random.Range(30, 91);
        item.defense = 0;
        item.weight = Random.Range(5.0f, 9.0f);
        item.durability = Random.Range(25.0f, 100.0f);
        item.itemSprite = weaponSprites[Random.Range(0, maxSprites)];
    }

    private void addRandomArmor()
    {
        item.itemName = armorNames[Random.Range(0, 10)];
        item.itemType = Item.itemsType.armor;
        item.itemSubType = (Item.itemsSubType)Random.Range(3, 6);
        item.itemLevel = Random.Range(1, 101);
        item.damage = 0;
        item.defense = Random.Range(30, 91);
        item.weight = Random.Range(10.0f, 19.0f);
        item.durability = Random.Range(25.0f, 100.0f);
        item.itemSprite = armorSprites[Random.Range(0, maxSprites)];
    }

    private void UpdateIndex(int index)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemSlot == index)
            {
                item = new Item();
                item = items[i];
                deleteItem(items[i].itemSlot, items[i]);
            }
        }
    }
}
