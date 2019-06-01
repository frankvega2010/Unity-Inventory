using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //public InventoryList items;
    //public string serializedData = "";

    //private int slot;
    //private int currentSlot;
    //private int maxSlots = 5;
    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        foreach (ScriptableObjectItem item in items.Inventory)
    //        {
    //            currentSlot++;
    //            Debug.Log(currentSlot);
    //            Debug.Log(item.itemName);
    //            Debug.Log(item.type);
    //            Debug.Log(item.damage);
    //            Debug.Log(item.weight);
    //        }
    //        currentSlot = 0;
    //        serializedData = items.Serialize();
    //        PlayerPrefs.SetString("inventory",serializedData);
    //        PlayerPrefs.Save();
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        serializedData = PlayerPrefs.GetString("inventory");

    //        if (PlayerPrefs.HasKey("inventory"))
    //        {
    //            items.Deserialize(serializedData);
    //        }
    //    }
    //}

    //public void addSlot()
    //{
    //    slot++;
    //}

    //public int getSlots()
    //{
    //    return slot;
    //}

    //public int getMaxSlots()
    //{
    //    return maxSlots;
    //}
}
