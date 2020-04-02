using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
   public bool attack = false;
   public Animator anim;

    CharacterBatControl batControl;

    void Awake()
    {
        batControl = GetComponentInParent<CharacterBatControl>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag ("Player"))
        {
            attack = true;
            anim.SetBool("Attack", attack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attack = false;
            anim.SetBool("Attack", attack);
        }
    }

}
