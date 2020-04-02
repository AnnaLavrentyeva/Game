using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementView : MonoBehaviour
{
    public Animator animator;
    public Transform WeaponParent;

    private CharacterMovementModel movementModel;

    void Awake()
    {
        movementModel = GetComponent<CharacterMovementModel>();

        if (animator == null) {
            Debug.LogError("Character animator wasn't set up");
            enabled = false;
        }
    }

    // Update is called once per frame
   public void Update()
    {
        UpdateDirection();
        UpdatePickUpAnimation();
        UpdateHit();
    }

    void UpdatePickUpAnimation()
    {
        bool pickUpOneHand = false;
        bool pickUpTwoHands= false;

        if (movementModel.IsFrozen() == true)
        {
            ItemType pickupItem = movementModel.GetItemThatIsBeingPickUp();

            if (pickupItem != ItemType.None)
            {
                ItemData itemData = Database.Item.FindItem(pickupItem);

                switch (itemData.getPickUpAnimation(movementModel.GetPickUpType()))
                {
                    case ItemData.PickUpAnim.OneHand:
                        pickUpOneHand = true;
                        break;
                    case ItemData.PickUpAnim.TwoHands:
                        pickUpTwoHands = true;
                        break;
                }

            }
        }

        animator.SetBool("PickUpOneHand", pickUpOneHand);
        animator.SetBool("PickUpTwoHands", pickUpTwoHands);
    }


    void UpdateDirection() {
        Vector3 direction = movementModel.GetDirection();

        if (direction != Vector3.zero) 
        {
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }

        animator.SetBool("IsMoving", movementModel.IsMoving());
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void OnAttackStarted()
    {
     //   SetWeaponActive(true);
    }

    public void OnAttackFinished()
    {
       // SetWeaponActive(false);
    }

    void SetWeaponActive(bool doActivate)
    {
        if(WeaponParent == null)
        {
            return;
        }

        for(int i = 0; i < WeaponParent.childCount; ++i)
        {
            WeaponParent.GetChild(i).gameObject.SetActive(doActivate);
        }
    }

    public void ShowWeapon()
    {
        SetWeaponActive(true);
    }

    public void HideWeapon()
    {
        SetWeaponActive(false);
    }

    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        SetSortingOrderOfItem(movementModel.WeaponParent, sortingOrder);
    }

    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        SetSortingOrderOfItem(movementModel.PickupItemParent, sortingOrder);
    }

    void SetSortingOrderOfItem(Transform itemParent, int sortingOrder)
    {
        if (itemParent == null)
        {
            return;
        }

        SpriteRenderer[] spriteRenderers = itemParent.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }




    //public void SetSortOrderLayer(int sortOrder)
    //{
    //    if (WeaponParent == null)
    //    {
    //        return;
    //    }

    //    SpriteRenderer[] spriteRenders = WeaponParent.GetComponentsInChildren<SpriteRenderer>();

    //    for(int i=0; i < spriteRenders.Length; ++i)
    //    {
    //        foreach (SpriteRenderer sprite in spriteRenders)
    //        {
    //            spriteRenders[i].sortingOrder = sortOrder;
    //        }
    //    }
    //}

    void UpdateHit()
    {
        animator.SetBool("IsHit", movementModel.IsPushed());
    }
}
