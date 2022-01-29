using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Inventory;

    public void GetOutOfStorage()
    {

    }

    public void AddInInventory(Transform item)
    {
        for(int i = 0; i < Inventory.transform.childCount; i++)
        {
            Transform child = Inventory.transform.GetChild(i);
            if(child.childCount < 1)
            {
                child.SetParent(item);
                break;
            }
        }
    }

    public void AddInStorage()
    {

    }

    public void MoveItem()
    {

    }

    public void Equip()
    {

    }

    public void Unequip()
    {

    }
}
