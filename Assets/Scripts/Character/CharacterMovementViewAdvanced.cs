using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementViewAdvanced : MonoBehaviour
{
    public Animator animator;
    CharacterMovementAdvancedModel advModel;

    void Awake()
    {
        advModel = GetComponent<CharacterMovementAdvancedModel>();
    }

    void FixedUpdate()
    {
        UpdatePush();
    }

    void UpdatePush()
    {

        animator.SetBool("Pushing", advModel.IsPushing());
        animator.SetBool("PushAndWalk", advModel.IsPushingAndWalking());
    }

}
