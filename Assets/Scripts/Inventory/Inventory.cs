using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public InventoryItemDatabase itemDatabase;
    public UIInventory uIInventory;

    void Start()
    {
        GiveItem(2);
        GiveItem(1);
        GiveItem(0);
        GiveItem("Bombs");
       // RemoveItem(1);
        uIInventory.gameObject.SetActive(!uIInventory.gameObject.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            uIInventory.gameObject.SetActive(!uIInventory.gameObject.activeSelf);
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        uIInventory.AddNewItem(itemToAdd);
  //      Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            uIInventory.RemoveItem(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.title);
        }
    }
}
