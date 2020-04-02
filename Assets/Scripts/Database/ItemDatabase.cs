using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Data;

    public ItemData FindItem(ItemType itemType)
    {
        for(int i=0; i< Data.Count; ++i)
        {
            if(Data[i].type == itemType)
            {
                return Data[i];
            }
        }

        return null;
    }

}

[System.Serializable]
public class ItemData
{
    public ItemType type;
    public GameObject prefab;
    public EquipPosition isEquipable;
    public PickUpAnim Animation;
    public PickUpAnim AnimationForDrops;

    public PickUpAnim getPickUpAnimation(PickUpType pickUpType)
    {
        switch (pickUpType)
        {
            case PickUpType.FromChest:
                return Animation;
              
            case PickUpType.FromDrop:
                return AnimationForDrops;               
        }
        return PickUpAnim.None;
    }

    public enum PickUpAnim
    {
        None,
        OneHand,
        TwoHands,
    }

    public enum EquipPosition
    {
        NotEquipable,
        SwordHand,
        ShieldHand
    }
}
