using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    public GameObject ShopMenu;
    public CharacterMovementModel movementModel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopMenu.SetActive(true);
        movementModel.SetFrozen(true, true, true);
    }

}
