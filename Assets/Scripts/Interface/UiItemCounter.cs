using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemCounter : MonoBehaviour
{
    public CharacterInventoryModel inventory;
    public ItemType itemType;
    public string NumberFormat;

    Text textDisplayed;

    void Awake()
    {
        textDisplayed = GetComponent<Text>();    
    }

    void Update()
    {
        if(inventory == null)
        {
            return;
        }
        textDisplayed.text = inventory.GetItemCount(itemType).ToString(NumberFormat);
    }

}
