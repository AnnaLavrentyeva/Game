using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Has all references 

    public CharacterMovementModel Movement;
    public CharacterInteractionModel Interaction;
    public CharacterMovementView MovementView;
    public CharacterInventoryModel InventoryModel;
    public CharacterHealth Health;
    public CharacterActionModel action;
}
