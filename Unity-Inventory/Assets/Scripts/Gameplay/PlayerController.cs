using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerInventoryGameObject;
    public GameObject playerInventoryUI;
    public int playerLevel;
    public int index;

    private Inventory playerInventory;
    public bool inventoryIsOpen;
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
                        playerInventory.addWeapon("", true);
                        break;
                    case 1:
                        playerInventory.addArmor("", true);
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
                playerInventory.addWeapon(index,"1", true);
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
