using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryModel : MonoBehaviour
{
    AudioSource source;
    public AudioClip getTheSword;
    public AudioClip getTheBomb;
    public AudioClip getTheRupee;
    public AudioClip getTheHeart;

    CharacterMovementModel movementModel;

    public CharacterHealth characterHealth;
    Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

    void Awake()
    {
        source = GetComponent<AudioSource>();
        movementModel = GetComponent<CharacterMovementModel>();    
    }

    public void AddItem(ItemType itemType, PickUpType pickupType)
    {
        AddItem(itemType, 1, pickupType);
    }

    public void AddItem(ItemType itemType, int amount, PickUpType pickupType)
    {
        if (items.ContainsKey(itemType) == true)
        {
            items[itemType] += amount;
        }
        else
        {
            items.Add(itemType, amount);
        }

     //   Debug.Log("Adding + " + itemType + "  " + amount);

        if (amount > 0)
        {
            ItemData itemData = Database.Item.FindItem(itemType);

            if (itemData != null)
            {
                if(itemData.getPickUpAnimation(pickupType) != ItemData.PickUpAnim.None)
                {
                    movementModel.ShowItemPickedUp(itemType, pickupType);
                }
                if (itemData.isEquipable == ItemData.EquipPosition.SwordHand)
                {
                movementModel.EquipWeapon(itemType);
                    source.clip = getTheSword;
                    source.Play();
                }
                if (itemData.isEquipable == ItemData.EquipPosition.ShieldHand)
                {
                movementModel.EquipShield(itemType);
                   
                }

                if ( itemData.type == ItemType.Bomb)
                {
                    source.clip = getTheBomb;
                    source.Play();
                }

                if (itemData.type == ItemType.Rupee)
                {
                    source.clip = getTheRupee;
                    source.Play();
                }
            }

            if (itemData.type == ItemType.Heart)
            {
                PlayHeartSound();
                // if (characterHealth.GetHealth() > 80)
                if (GameObject.Find("Character").GetComponent<CharacterHealth>().GetHealth() > 80)
                {
                    GameObject.Find("Character").GetComponent<CharacterHealth>().health = 100;
                }
                else
                {
                    GameObject.Find("Character").GetComponent<CharacterHealth>().AddHealth(20);
                }
            }
        }
    }

    public int GetItemCount(ItemType itemType)
    {
        if(items.ContainsKey(itemType) == false)
        {
            return 0;
        }
        return items[itemType];
    }

    public void PlayHeartSound()
    {
        source.clip = getTheHeart;
        source.Play();
    }
}
