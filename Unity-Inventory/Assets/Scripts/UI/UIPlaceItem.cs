using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlaceItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void OnItemAction(int index);
    public OnItemAction OnItemPointerEnter;
    public OnItemAction OnItemPointerExit;

    public GameObject Inventory;
    public int currentIndex;
    private Inventory playerInventory;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnItemPointerEnter != null)
        {
            Image iconImage = gameObject.GetComponent<Image>();
            iconImage.color = Color.red;
            OnItemPointerEnter(GetComponent<SlotIndex>().index3);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnItemPointerExit != null)
        {
            Image iconImage = gameObject.GetComponent<Image>();
            iconImage.color = new Vector4(0, 0, 0, 0);
            OnItemPointerExit(GetComponent<SlotIndex>().index3);
        }
    }
}
