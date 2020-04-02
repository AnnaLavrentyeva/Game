using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItemProbability
{
    public ItemType DropItemType;
    public int Amount;

    [Range(0f, 1f)]
    public float Probability;
}
public class DropItems : MonoBehaviour
{
    public DropItemProbability[] Probabilities;
    public Transform DropPosition;

    void LootDrop()
    {
        for (int i = 0; i < Probabilities.Length; ++i)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue > Probabilities[i].Probability)
            {
                continue;
            }
            DropItem(Probabilities[i].DropItemType);

            return;
        }
    }

    void DropItem(ItemType itemType)
    {
        ItemData itemData = Database.Item.FindItem(itemType);
        if (itemData == null || itemData.prefab == null)
        {
            Debug.LogWarning("Can't prop " + itemType + " no data found in DB.");
            return;
        }

        Transform dropPosition = DropPosition;

        if(dropPosition == null)
        {
            return;
        }
        Instantiate(itemData.prefab, dropPosition.position, Quaternion.identity);
    }
}
