using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject InventoryUIGameObject;
    private InventoryUI inventoryUIPlayer;
    private void Start()
    {
        inventoryUIPlayer = InventoryUIGameObject.GetComponent<InventoryUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("IT WILL DISSAPPEAR");
        inventoryUIPlayer.isIconOutOfInventoryBounds = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("IT WONT DISSAPPEAR");
        inventoryUIPlayer.isIconOutOfInventoryBounds = false;
    }
}
