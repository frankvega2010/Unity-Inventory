using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryList
{
    public List<ScriptableObjectItem> Inventory;

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }

    public void Deserialize(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}