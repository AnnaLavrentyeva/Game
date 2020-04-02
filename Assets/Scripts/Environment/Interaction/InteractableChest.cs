using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChest : Interactable
{
    public Sprite OpenChestSprite;
    public ItemType ItemInChest;
    public int Amount;

    private bool isOpen = false;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnInteract(Character character)
    {
        if(isOpen == true)
        {
            return;
        }
        character.InventoryModel.AddItem(ItemInChest, Amount, PickUpType.FromChest);
        spriteRenderer.sprite = OpenChestSprite;
        isOpen = true;

    }

}
