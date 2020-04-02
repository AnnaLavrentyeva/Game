using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBatControl : CharacterBaseControl
{
    public float pushStrength;
    public float pushTime;

    GameObject characterInRangeObj;
    bool isCharacterInRange;

    public AttackableEnemy attackableEnemy;
    public float AttackDmg;

    void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;

        if (characterInRangeObj != null && gameObject.tag=="JumpEnemy")
        {
            direction = characterInRangeObj.transform.position - transform.position;
            direction.Normalize();
            Vector2.MoveTowards(transform.position, direction, 1*Time.deltaTime);

        }


        if (characterInRangeObj != null && gameObject.tag != "JumpEnemy")
        {
            direction = characterInRangeObj.transform.position - transform.position;
            direction.Normalize();
        }

        if(attackableEnemy !=null && attackableEnemy.GetHealth() <= 0)
        {
            direction = Vector2.zero;
        }
        SetDirection(direction);
    }

    public void SetCharacterInRange(GameObject characterInRange)
    {
        characterInRangeObj = characterInRange;
    }

    public void HitCharacter(Character character )
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();
        characterInRangeObj = null;
        character.Movement.PushCharacter(direction* pushStrength, pushTime);

        character.Health.MakeDamage(AttackDmg);
    }
}
