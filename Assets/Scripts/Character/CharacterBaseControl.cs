using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseControl : MonoBehaviour
{

    protected Character character;

    void Awake()
    {
        character = GetComponent<Character>();

    }

    protected void SetDirection(Vector2 direction)
    {
        if(character.Movement == null)
        {
            return;
        }
        character.Movement.SetDirection(direction);
    }

    protected void OnActionPressed()
    {
        if(character.Interaction == null)
        {
            return;
        }
        character.Interaction.OnInteract();
        
    }

    protected void OnAttackPressed()
    {

        if (character.Movement == null)
        {
            return;
        }

        if(character.Movement.CanAttack() == false)
        {
            return;
        }
        character.Movement.Attack();
        character.MovementView.Attack();
    }

     protected void OnPlaceBombPressed()
    {
        if(character.action == null)
        {
            return;
        }
        character.action.PlaceBomb();
    }
}
