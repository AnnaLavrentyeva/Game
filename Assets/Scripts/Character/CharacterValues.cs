using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValues: MonoBehaviour
{

    public static int rupee;
    public static int bombs;
    public static int damage = 10;

    static bool inDungeonOne;
    static bool inMain;

    static Vector3 fromMainToDungeon;
    public CharacterMovementModel movementModel;
    public ItemType itemType;


    public int rps;
    public int bmb;
    public int dmg;
    public bool swrd;
    public bool setSwrd;

    private void Awake()
    {
        rps = rupee;
        bmb = bombs;
        dmg = damage;
        movementModel = GetComponent<CharacterMovementModel>();

        fromMainToDungeon = new Vector3(-25.4f, 19.2f, 0);
    }

    private void Update()
    {
        if (inDungeonOne && !inMain)
        {
            Delete();
        }

        if (!inDungeonOne && inMain)
        {
            rupee = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Rupee);
            bombs = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Bomb);

        }
        dmg = damage;
    }

    public void SetBoolFromPlot()
    {
        inDungeonOne = false;
        inMain = true;
    }

    public void SetBoolFirstDungeon()
    {
        inDungeonOne = true;
        inMain = false;
        rupee = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Rupee);
        bombs = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Bomb);
        damage = dmg;
      //  movementModel.EquipWeapon(itemType);

    }

    

    public void ExitFromDungeon()
    {
        rupee = rps;
        bombs = bmb;
      //  GameObject.Find("Character").GetComponent<CharacterInventoryModel>().AddItem(ItemType.Rupee, rupee, PickUpType.None);
    }

    public void UpdateRps()
    {
        rupee = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Rupee);
        bombs = GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Bomb);
        bmb = bombs;
        rps = rupee;
    }
    public void RemoveFromTotalRupees(int value)
    {
        rupee -= value;
    }

    public void AddTotalBombs(int value)
    {
        bombs += value;
    }

    void Delete()
    {
        inDungeonOne = false;
        inMain = false;
    }

    public void SetNewDamage(int damg)
    {
        damage = damg;
    }
}

