using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Character))]
public class CharacterInteractionModel : MonoBehaviour
{
    private Character character;
    private Collider2D m_collider;
    private CharacterMovementModel movementModel;

    InteractablePickup pickedUpObject;

    AudioSource source;
    public AudioClip throwSound;

    void Awake()
    {
        character = GetComponent<Character>();
        m_collider = GetComponent<Collider2D>();
        movementModel = GetComponent<CharacterMovementModel>();
        source = GetComponent<AudioSource>();
    }

    public void OnInteract()
    {
        if(isCarryingObject() == true)
        {
            ThrowObject();
            return;
        }
        Interactable usableInteractable = FindUsableInteractable();
        if (usableInteractable == null)
        {
            return;
        }
        usableInteractable.OnInteract(character);
     //   Debug.Log("Found Interactable " + usableInteractable.name);

    }
    
    public Collider2D[] GetCloseColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        return Physics2D.OverlapAreaAll(
            (Vector2)transform.position + boxCollider.offset + boxCollider.size * 0.6f,
            (Vector2)transform.position + boxCollider.offset - boxCollider.size * 0.6f);
            
    }

    Interactable FindUsableInteractable()
    {
     
        Collider2D[] closeColliders = GetCloseColliders();     
        Interactable closestInteractable = null;
        float angleToClosestInteractable = Mathf.Infinity;
        
        for (int i = 0; i < closeColliders.Length; ++i)
        {
            Interactable colliderInteractable = closeColliders[i].GetComponent<Interactable>();

            if(colliderInteractable == null)
            {
                continue;
            }
            Vector3 directionToInteractable = closeColliders[i].transform.position - transform.position;
            float angleToInteractable = Vector3.Angle(movementModel.GetFacingDirection(), directionToInteractable);


            if (angleToInteractable < 40)
            {
                if (angleToInteractable < angleToClosestInteractable)
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractable = angleToInteractable;
                }
            }
        }

        return closestInteractable;
    }

    public void PickUpObject(InteractablePickup pickUpObject)
    {
        pickedUpObject = pickUpObject;
        pickedUpObject.transform.parent = movementModel.PickupItemParent;
        pickedUpObject.transform.localPosition = Vector3.zero;
        movementModel.SetAbleToAttack(false);
        Helper.SetSortingLayerToSprites(pickUpObject.transform, "Characters");
        // Physics2D.IgnoreCollision(m_collider, pickUpObject.GetComponent<Collider2D>());
        Collider2D pickupObjectCollider = pickedUpObject.GetComponent<Collider2D>();

        if (pickupObjectCollider != null)
        {
            pickupObjectCollider.enabled = false;
        }

    }

    public bool isCarryingObject()
    {
        return pickedUpObject != null;
    }

    public void ThrowObject()
    {
        source.clip = throwSound;
        source.Play();

        Collider2D pickupObjectCollider = pickedUpObject.GetComponent<Collider2D>();

        if (pickupObjectCollider != null)
        {
            pickupObjectCollider.enabled = true;
            Physics2D.IgnoreCollision(m_collider, pickupObjectCollider);
        }

        pickedUpObject.Throw(character);
        pickedUpObject = null;
        movementModel.SetAbleToAttack(true);
    }

}
