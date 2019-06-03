using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject playerInventoryGameObject;
    public GameObject playerInventoryUI;

    [Header("Player Info")]
    public int playerLevel;
    public bool inventoryIsOpen;

    [Header("Spawn Item Info")]
    public string itemName;
    public int index;

    //private Item.itemsType itemType;
    private Inventory playerInventory;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = playerInventoryGameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inventoryIsOpen)
            {
                int randomNumber = Random.Range(0, 2);
                switch (randomNumber)
                {
                    case 0:
                        playerInventory.addItem(-1,"", true,Item.itemsType.weapon);
                        break;
                    case 1:
                        playerInventory.addItem(-1,"", true, Item.itemsType.armor);
                        break;
                    default:
                        break;
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(inventoryIsOpen)
            {
                playerInventory.addItem(index,itemName, true, Item.itemsType.none);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenCloseInventory();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            playerInventory.deleteItem(index);
        }
    }

    public void OpenCloseInventory()
    {
        inventoryIsOpen = !inventoryIsOpen;
        playerInventoryGameObject.SetActive(inventoryIsOpen);
        playerInventoryUI.SetActive(inventoryIsOpen);
    }
}
