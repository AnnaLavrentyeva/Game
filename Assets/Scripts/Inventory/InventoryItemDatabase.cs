using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDatabase : MonoBehaviour
{
    public CharacterInventoryModel inventory;
    int amountOfBombs;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        amountOfBombs = inventory.GetItemCount(ItemType.Bomb);
        BuildDatabase();
    }
    private void Update()
    {
        amountOfBombs = inventory.GetItemCount(ItemType.Bomb);
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }
    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>
        {
            new Item(0, "Sword from the stump", "You found it in the cut tree. It doesn't look powerfull. Is it for squirrels?",
            new Dictionary<string, int>
            {
                {"Power: ", 10 },
                {"Defence: ", 0 }
            }),
            new Item(1, "Bombs", "I don't know where why you always with bombs. I try NOT to think about it...",
            new Dictionary<string, int>
            {
                {"Damage: ", 40 },
                {"Time before the explosion: ", 4 }
            }),
            new Item(2, "Diamond ore", "It is always with you",
            new Dictionary<string, int>
            {
                {"Amount: ", 1 }
            }),
            new Item(3, "Pink sword", "You bought it in the strange shop",
            new Dictionary<string, int>
            {
                {"Amount: ", 1 }
            })
        };
    }

}
