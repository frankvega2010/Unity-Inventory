using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public delegate void InventoryAction(int index);
    public InventoryAction InventoryDragItem;
    
    [Header("Game Objects")]
    public GameObject[] iconsGroup;
    public List<GameObject> equipmentSlots;
    public GameObject newIcon;
    public GameObject InventoryGameObject;

    [Header("New Icon Settings")] //its not "Panel" , it should be dragIcon.
    public int maxIcons;
    public Transform newParent;
    public bool isIconOutOfInventoryBounds;

    [Header("Icon Data")]
    public Color invisibleColor;
    public Color newColor;
    public Sprite oldSprite;
    public int oldIndex;


    private Inventory playerInventory;
    private GameObject instancedPanel;
    private RectTransform instancedItemRect;
    private UIDragItem draggedNewItem; //

    private string noNameGiven = "";
    private bool existingItem = false;

    private void Start()
    {
        for (int i = 0; i < maxIcons - equipmentSlots.Count; i++)
        {
                Debug.Log("instanciated");
                GameObject newItemUI = Instantiate(newIcon);
                newItemUI.transform.SetParent(gameObject.transform);
                newItemUI.SetActive(true);

                iconsGroup[i] = newItemUI;
                iconsGroup[i].GetComponent<SlotIndex>().index = i;

                UIDragItem DraggedItem = iconsGroup[i].GetComponent<UIDragItem>();
                UIPlaceItem PlaceItem = iconsGroup[i].GetComponent<UIPlaceItem>();

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

            iconsGroup[maxIcons - equipmentSlots.Count + i] = equipmentSlots[i];
            iconsGroup[maxIcons - equipmentSlots.Count + i].GetComponent<SlotIndex>().index = maxIcons - equipmentSlots.Count + i;
        }

        playerInventory = InventoryGameObject.GetComponent<Inventory>();

        draggedNewItem = newIcon.GetComponent<UIDragItem>();
        draggedNewItem.OnItemDrag = OnItemDrag;
    }

    private void Update()
    {
        if (draggedNewItem != null)
        {
            if (draggedNewItem.isMoving)
            {
                newIcon.transform.position = Input.mousePosition;
            }
        } 
    }

    public GameObject addItem(Sprite spriteToAdd, int index)
    {
        if (iconsGroup[index] != null)
        {
            Image newSprite = iconsGroup[index].GetComponent<Image>();
            newSprite.sprite = spriteToAdd;
            newSprite.color = newColor;
            iconsGroup[index].GetComponent<UIPlaceItem>().enabled = false;

            return iconsGroup[index];
        }

        return null;
    }

    public void removeItem(int index)
    {

        Image currentSprite = iconsGroup[index].GetComponent<Image>();
        iconsGroup[index].GetComponent<UIPlaceItem>().enabled = true;

        currentSprite.color = invisibleColor;
        currentSprite.sprite = oldSprite;
    }

    public void OnItemBeginDrag(int index)
    {

        if (iconsGroup[index].GetComponent<Image>().sprite != oldSprite)
        {
            newIcon.SetActive(true);
            oldIndex = index;


            Image newItemUIImage = newIcon.GetComponent<Image>();
            instancedItemRect = newIcon.GetComponent<RectTransform>();
            newIcon.name = "New Item";
            newIcon.transform.SetParent(newParent);
            draggedNewItem.isMoving = true;
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
        if (iconsGroup[index].GetComponent<Image>().sprite == oldSprite && newIcon.activeSelf)
        {
            if (playerInventory.newIndex == 30) // switch to -1
            {
                if (!isIconOutOfInventoryBounds)
                {
                    playerInventory.addItem(oldIndex,noNameGiven,existingItem,Item.itemsType.none);
                }
                else
                {
                    // put item on 3D World...
                }
            }
            else
            {
                if (playerInventory.addItem(playerInventory.newIndex, noNameGiven, existingItem, Item.itemsType.none))
                {
                    newIcon.GetComponent<SlotIndex>().index = playerInventory.newIndex;
                }
                else
                {
                    playerInventory.addItem(oldIndex, noNameGiven, existingItem, Item.itemsType.none);
                }
                
            }
            draggedNewItem.isMoving = false;
            Debug.Log("drag ended");

            newIcon.SetActive(false);
        }
    }

    public void OnItemDrag(Vector3 position)
    {
        Debug.Log("asdd42");
        newIcon.transform.position = position;
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
