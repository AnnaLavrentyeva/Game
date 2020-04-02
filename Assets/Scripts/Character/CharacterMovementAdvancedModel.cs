using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementAdvancedModel : MonoBehaviour
{
    public float PushingSpeed;

    CharacterMovementModel characterMovement;
    CharacterInteractionModel m_InteractionModel;

    Vector3 m_LastPosition;
    float m_LastPositionTime;
    float m_MovementStartTime;
    bool m_WasMoving;
    Pushable closestPushable;
    Transform closestPushableParent;
    Collider2D col;

    void Awake()
    {
        characterMovement = GetComponent<CharacterMovementModel>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        UpdatePushing();
        UpdateWasMoving();
        UpdatePushableObjects();
    }

    void UpdateWasMoving()
    {
        if (m_WasMoving == false && characterMovement.IsMoving() == true)
        {
            m_MovementStartTime = Time.realtimeSinceStartup;
        }

        m_WasMoving = characterMovement.IsMoving();
    }

    void UpdatePushableObjects()
    {
        if (closestPushable != null)
        {
            if (characterMovement.IsMoving() == false)
            {
                StopPushingClosestPushable();
            }

            return;
        }

        if (IsPushing() == false)
        {
            return;
        }

        closestPushable = FindClosestPushable();

        if (closestPushable == null)
        {
            return;
        }

        StartPushingClosestPushable();
    }


    void StartPushingClosestPushable()
    {
        closestPushableParent = closestPushable.transform.parent;
        closestPushable.transform.parent = transform;

        Collider2D closestPushableCollider = closestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, closestPushableCollider);

        characterMovement.SetFrozen(false, true, false);
        characterMovement.SetOverrideSpeed(true, PushingSpeed);
    }



    void StopPushingClosestPushable()
    {
        Collider2D closestCollider = closestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, closestCollider, false);
       
        closestPushable.transform.parent = null;
        closestPushable = null;

        characterMovement.SetFrozen(false, false, false);
        characterMovement.SetOverrideSpeed(false);
    }



    Pushable FindClosestPushable()
    {
        Collider2D[] closeColliders = m_InteractionModel.GetCloseColliders();
        Pushable closestPushable = null;
        float angleToClosestPushable = Mathf.Infinity;

        for (int i = 0; i < closeColliders.Length; ++i)
        {
            Pushable colliderPushable = closeColliders[i].GetComponent<Pushable>();
            Debug.Log(colliderPushable);

            if (colliderPushable == null)
            {
                continue;
            }
            Vector3 directionToPushable = closeColliders[i].transform.position - transform.position;

            float angleToPushable = Vector3.Angle(characterMovement.GetFacingDirection(), directionToPushable);

            if (angleToPushable < 40)
            {
                if (angleToPushable < angleToClosestPushable)
                {
                    closestPushable = colliderPushable;
                    angleToClosestPushable = angleToPushable;
                }
            }
        }
        return closestPushable;
    }

    void UpdatePushing()
    {
        Vector3 position = transform.position;

        if (Vector3.Distance(position, m_LastPosition) > 0.005f)
        {
            m_LastPosition = position;
            m_LastPositionTime = Time.realtimeSinceStartup;
        }
    }

    float GetMovingDuration()
    {
        if (characterMovement.IsMoving() == false)
        {
            return 0f;
        }

        return Time.realtimeSinceStartup - m_MovementStartTime;
    }

    float GetTimeInSamePosition()
    {
        return Time.realtimeSinceStartup - m_LastPositionTime;
    }

    public bool IsPushing()
    {
        if (characterMovement.IsMoving() == false || m_WasMoving == false)
        {
            return false;
        }

        if (closestPushable != null)
        {
            return true;
        }

        if (characterMovement.IsFrozen() == true)
        {
            return false;
        }

      
        return GetMovingDuration() > 0.1f &&
               GetTimeInSamePosition() > 0.1f;
    }

    public bool IsPushingAndWalking()
    {
        if (IsPushing() == false)
        {
            return false;
        }

        return true;
        // return GetTimeInSamePosition() > 0.1f; // to stop while push to object near
    }
}

