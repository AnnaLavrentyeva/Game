using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionView : MonoBehaviour
{
    public Animator animator;
    CharacterInteractionModel model;

    void Awake()
    {
        model = GetComponent<CharacterInteractionModel>();
    }

    void Update()
    {
        UpdateIsCarryingObject();
    }

    void UpdateIsCarryingObject()
    {
        animator.SetBool("IsCarrying", model.isCarryingObject());
    } 
}
