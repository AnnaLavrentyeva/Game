using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : Interactable
{
    public float throwDist = 5;
    public float throwSpeed = 3;
    Vector3 characterThroePosition;
    Vector3 throwDirection;

    bool throwing = false;
    public ItemType type;


    public override void OnInteract(Character character)
    {
        CharacterInteractionModel interactionModel = character.GetComponent<CharacterInteractionModel>();

        if(interactionModel == null)
        {
            return;
        }

        BroadcastMessage("OnPickUpObject", character, SendMessageOptions.DontRequireReceiver);
        interactionModel.PickUpObject(this);
    }
    
    public void Throw(Character character)
    {
        StartCoroutine(ThrowRoutine(character.transform.position, character.Movement.GetFacingDirection()));
    }
    IEnumerator ThrowRoutine(Vector3 characterThrowPosition, Vector3 throwDirection)
    {
        transform.parent = null;
        Vector3 taregetPosition = characterThrowPosition + throwDirection.normalized * throwDist;
        throwing = true;
        while (transform.position != taregetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, taregetPosition, throwSpeed * Time.deltaTime);
            yield return null;
        }
        throwing = true;
        BroadcastMessage("OnObjectThrown", SendMessageOptions.DontRequireReceiver);
        throwing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attackable attackable = collision.gameObject.GetComponent<Attackable>();

        if (attackable != null && throwing)
        {
            attackable.Hit(gameObject.GetComponent<Collider2D>(), type);
            throwing = false;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy" && throwing)
    //    {
    //        Debug.Log("Hit");
    //    }
        
    //}
}
