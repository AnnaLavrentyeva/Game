using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopMenu;
    Character character;
    public CharacterInventoryModel inventory;
    public CharacterMovementModel movementModel;
    public CharacterHealth health;

    public AttackableEnemy attackableEnemy;

    public Inventory uiInventory;

    public bool pinkSword = false;

    public ItemType ItemInChest;
    public int Amount;

    private void Awake()
    {
        character = GetComponent<Character>();
        attackableEnemy = GetComponent<AttackableEnemy>();
    }

    public void BuyBomb()
    {
        
        if (inventory.GetItemCount(ItemType.Rupee) > 4)
        {
            inventory.AddItem(ItemType.Bomb, 1, PickUpType.None);
            inventory.AddItem(ItemType.Rupee, -5, PickUpType.None);
            GameObject.Find("Values").GetComponent<CharacterValues>().RemoveFromTotalRupees(5);
            GameObject.Find("Values").GetComponent<CharacterValues>().AddTotalBombs(1);
        }

    }

    public void BuyHeart()
    {
        if (inventory.GetItemCount(ItemType.Rupee) > 24 && health.GetHealth() < 100)
        {
            inventory.AddItem(ItemType.Rupee, -25, PickUpType.None);
            GameObject.Find("Values").GetComponent<CharacterValues>().RemoveFromTotalRupees(25);


            if (health.GetHealth() <= 80)
            {
                health.AddHealth(20);
            }
            if(health.GetHealth() > 80)
            {
                health.AddHealth(health.GetMaxHealth() - health.GetHealth());
            }
        }
    }

    public void BuySword()
    {
        if (inventory.GetItemCount(ItemType.Rupee) > 99)
        {
            inventory.AddItem(ItemType.Rupee, -100, PickUpType.None);
            pinkSword = true;
            Debug.Log(pinkSword);
            uiInventory.GiveItem(3);
            movementModel.Unequip();
            GameObject.Find("Values").GetComponent<CharacterValues>().RemoveFromTotalRupees(100);
            GameObject.Find("Values").GetComponent<CharacterValues>().SetNewDamage(20);
        }
    }
   

    public void GoBack()
    {
        ShopMenu.SetActive(false);
        movementModel.SetFrozen(false, false, true);
    }
}
