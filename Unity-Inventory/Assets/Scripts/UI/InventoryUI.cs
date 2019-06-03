﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public delegate void InventoryAction(int index);
    public InventoryAction InventoryDragItem;

    public GameObject panel;
    public List<GameObject> equipmentSlots;
    public bool isPanelOutOfInventoryBounds;
    public Color invisibleColor;
    public Color newColor;
    public Sprite oldSprite;
    public int maxPanels;
    public GameObject[] panelGroup;
    public int oldIndex;
    public Transform newParent;

    public GameObject InventoryGameObject;
    private Inventory playerInventory;

    public GameObject EquipmentInventoryGameObject;
    private Inventory EquipmentplayerInventory;

    private GameObject instancedPanel;
    private RectTransform instancedItemRect;
    private void Start()
    {
        //int maxPanels = 28;
        for (int i = 0; i < maxPanels - equipmentSlots.Count; i++)
        {
                Debug.Log("instanciated");
                GameObject newItemUI = Instantiate(panel);
                newItemUI.transform.SetParent(gameObject.transform);
                newItemUI.SetActive(true);

                panelGroup[i] = newItemUI;
                panelGroup[i].GetComponent<PanelIndex>().index3 = i;

                UIDragItem DraggedItem = panelGroup[i].GetComponent<UIDragItem>();
                UIPlaceItem PlaceItem = panelGroup[i].GetComponent<UIPlaceItem>();

                DraggedItem.OnItemBeginDrag = OnItemBeginDrag;
                DraggedItem.OnItemEndDrag = OnItemEndDrag;

                PlaceItem.OnItemPointerEnter = OnItemPointerEnter;
                PlaceItem.OnItemPointerExit = OnItemPointerExit;
                PlaceItem.enabled = true;

            
        }

        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            Debug.Log("instanciated2");
            UIDragItem DraggedItem = equipmentSlots[i].GetComponent<UIDragItem>();
            UIPlaceItem PlaceItem = equipmentSlots[i].GetComponent<UIPlaceItem>();

            DraggedItem.OnItemBeginDrag = OnItemBeginDrag;
            DraggedItem.OnItemEndDrag = OnItemEndDrag;

            PlaceItem.OnItemPointerEnter = OnItemPointerEnter;
            PlaceItem.OnItemPointerExit = OnItemPointerExit;
            PlaceItem.enabled = true;

            panelGroup[maxPanels - equipmentSlots.Count + i] = equipmentSlots[i];
            panelGroup[maxPanels - equipmentSlots.Count + i].GetComponent<PanelIndex>().index3 = maxPanels - equipmentSlots.Count + i;
        }

        playerInventory = InventoryGameObject.GetComponent<Inventory>();

        UIDragItem DraggedItem2 = panel.GetComponent<UIDragItem>();
        DraggedItem2.OnItemDrag = OnItemDrag;
        //DraggedItem2.isIntance = true;
    }

    private void Update()
    {
        if (panel.GetComponent<UIDragItem>().isMoving)
        {
            panel.transform.position = Input.mousePosition;
        }
    }

    public GameObject addItem(Sprite spriteToAdd, int index)
    {
        if (panelGroup[index] != null)
        {
            Image newSprite = panelGroup[index].GetComponent<Image>();
            newSprite.sprite = spriteToAdd;
            newSprite.color = newColor;
            panelGroup[index].GetComponent<UIPlaceItem>().enabled = false;

            return panelGroup[index];
        }

        return null;
    }

    public void removeItem(int index)
    {

        Image currentSprite = panelGroup[index].GetComponent<Image>();
        panelGroup[index].GetComponent<UIPlaceItem>().enabled = true;

        currentSprite.color = invisibleColor;
        currentSprite.sprite = oldSprite;
    }

    public void OnItemBeginDrag(int index2)
    {

        if (panelGroup[index2].GetComponent<Image>().sprite != oldSprite)
        {
            panel.SetActive(true);
            oldIndex = index2;


            Image newItemUIImage = panel.GetComponent<Image>();
            instancedItemRect = panel.GetComponent<RectTransform>();
            panel.name = "New Item";
            panel.transform.SetParent(newParent);
            panel.GetComponent<UIDragItem>().isMoving = true;
            instancedItemRect.sizeDelta = new Vector2(60, 60);
            newItemUIImage.color = newColor;

            foreach (Item item in playerInventory.items)
            {
                if (item.itemSlot == oldIndex)
                {
                    newItemUIImage.sprite = item.itemSprite;
                }
            }



            if (InventoryDragItem != null)
            {

                for (int i = 0; i < playerInventory.items.Count; i++)
                {
                    if (playerInventory.items[i].itemSlot == oldIndex)
                    {
                        InventoryDragItem(playerInventory.items[i].itemSlot);
                        i = playerInventory.items.Count;
                    }
                }
            }

        }
    }


    public void OnItemEndDrag(int index)
    {
        if (panelGroup[index].GetComponent<Image>().sprite == oldSprite && panel.activeSelf)
        {
            if (playerInventory.newIndex == 30) // switch to -1
            {
                if (!isPanelOutOfInventoryBounds)
                {
                    playerInventory.addWeapon(oldIndex,"",false);
                }
                else
                {
                    // put item on 3D World...
                }
            }
            else
            {
                if(playerInventory.addWeapon(playerInventory.newIndex,"",false))
                {
                    panel.GetComponent<PanelIndex>().index3 = playerInventory.newIndex;
                }
                else
                {
                    playerInventory.addWeapon(oldIndex,"",false);
                }
                
            }
            panel.GetComponent<UIDragItem>().isMoving = false;
            Debug.Log("drag ended");

            panel.SetActive(false);
        }
    }

    public void OnItemDrag(Vector3 position)
    {
        Debug.Log("asdd42");
        panel.transform.position = position;
    }

    public void OnItemPointerEnter(int index)
    {
        playerInventory.newIndex = index;
        Debug.Log("New Index : " + playerInventory.newIndex);
    }

    public void OnItemPointerExit(int index)
    {
        playerInventory.newIndex = 30;
        Debug.Log("New Index : " + playerInventory.newIndex);
    }
}