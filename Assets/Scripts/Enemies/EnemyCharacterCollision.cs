using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour
{
    CharacterBatControl batControl;
    CharacterLobsterControl lobsterControl;

    void Awake()
    {
        batControl = GetComponentInParent<CharacterBatControl>();
        lobsterControl = GetComponentInParent<CharacterLobsterControl>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (gameObject.tag == "WalkEnemy")
            {
                batControl.HitCharacter(col.gameObject.GetComponent<Character>());
                lobsterControl.turnRight = !lobsterControl.turnRight;
            }
            else
            {
                batControl.HitCharacter(col.gameObject.GetComponent<Character>());
            }

        }
    }

}

