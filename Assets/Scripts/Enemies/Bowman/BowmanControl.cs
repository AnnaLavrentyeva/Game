using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowmanControl : CharacterBaseControl
{

    public float pushStrength;
    public float pushTime;

    GameObject characterInRangeObj;
    bool isCharacterInRange;

    public AttackableEnemy attackableEnemy;
    public float AttackDmg;

    public GameObject arrowPrefab;

    Vector2 direction = Vector2.zero;

    void Update()
    {
        UpdateDirection();
    }

    public void SetCharacter(GameObject characterInRange)
    {
        characterInRangeObj = characterInRange;
    }

    void UpdateDirection()
    {
        direction = Vector2.zero;

        if (characterInRangeObj != null)
        {
            direction = characterInRangeObj.transform.position - transform.position;
            direction.Normalize();
        }

        if (attackableEnemy != null && attackableEnemy.GetHealth() <= 0)
        {
            direction = Vector2.zero;
        }

        SetDirection(direction);

    }

}
