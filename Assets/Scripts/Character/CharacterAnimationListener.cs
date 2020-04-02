using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationListener : MonoBehaviour
{

    public CharacterMovementModel movementModel;
    public CharacterMovementView movementView;

    public void AttackStarted(AnimationEvent animationEvent)
    {
        if(movementModel != null) 
        {
            movementModel.AttackStarted();
        }

        if (movementView != null)
        {
            movementView.OnAttackStarted();
        }
        SetSortingOrderOfWeapon(animationEvent.intParameter);
    }

    public void AttackFinished()
    {
        if (movementModel != null)
        {
            movementModel.AttackFinished();
        }

        if (movementView != null)
        {
            movementView.OnAttackFinished();
        }
        HideWeapon();
    }

    public void ShowWeapon()
    {
        if (movementView != null)
        {
            movementView.ShowWeapon();
        }
    }

    public void HideWeapon()
    {
        if (movementView != null)
        {
            movementView.HideWeapon();
        }
    }

    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        if (movementView != null)
        {
            movementView.SetSortingOrderOfWeapon(sortingOrder);
        }
    }


    public void SetSortingOrderOfPickingUp(int sortingOrder)
    {
        if(movementModel != null)
        {
            movementView.SetSortingOrderOfPickupItem(sortingOrder);
        }
    }
}
