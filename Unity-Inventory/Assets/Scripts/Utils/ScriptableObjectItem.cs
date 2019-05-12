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
    public string type;
    public float damage;
    public float weight;
    public int id;
    //public Sprite imagen;
}
