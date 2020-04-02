using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    public GameObject EnterToThePacman;
    public CharacterInventoryModel inventory;
    public CharacterMovementModel movementModel;

    public void LoadPackman()
    {
        if (inventory.GetItemCount(ItemType.Rupee) > 49)
        {
            inventory.AddItem(ItemType.Rupee, -50, PickUpType.None);
            movementModel.SetFrozen(false, false, true);
            Application.LoadLevel("Pacman");

            GameObject.Find("Values").GetComponent<CharacterValues>().RemoveFromTotalRupees(50);

        }
    }

    public void GoBack()
    {
        EnterToThePacman.SetActive(false);
        movementModel.SetFrozen(false, false, true);
    }
}
