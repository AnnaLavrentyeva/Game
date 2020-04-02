using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour
{
    public CharacterCollision characterCollision;
    public CharacterHealth characterHealth;
    public Character character;
    public bool inside = false;

    public float pushStrength;
    public float pushTime;



    private void Update()
    {
      

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && characterCollision.attack == true)
        {
            inside = true;
        }
        StartCoroutine(WaitBeforeAttack());

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;
        }
    }

     
    private IEnumerator WaitBeforeAttack() 
    {
        yield return new WaitForSeconds(0.5f);
        if (inside)
        {

            GameObject.Find("Character").GetComponent<CharacterHealth>().MakeDamage(5);
            Vector2 direction = GameObject.Find("Character").transform.position - transform.position;
            direction.Normalize();

            GameObject.Find("Character").GetComponent<CharacterMovementModel>().PushCharacter(direction * pushStrength, pushTime);
        }
    }

}

