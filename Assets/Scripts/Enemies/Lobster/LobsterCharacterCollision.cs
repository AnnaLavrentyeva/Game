using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterCharacterCollision : MonoBehaviour
{
    CharacterLobsterControl lobsterControl;
    CharacterBatControl batControl;

    void Awake()
    {
        lobsterControl = GetComponentInParent<CharacterLobsterControl>();
        batControl = GetComponentInParent<CharacterBatControl>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            lobsterControl.speed = 3;

           // lobsterControl.HitCharacter(col.gameObject.GetComponent<Character>());
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lobsterControl.speed = 0;
        }
    }
}
