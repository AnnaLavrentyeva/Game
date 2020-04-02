
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGamepadControl : CharacterBaseControl
{
    void Update()
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) // .GetButtonDown("String");
        {
            OnActionPressed();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3)) // .GetButtonDown("String");
        {
            OnPlaceBombPressed();
        }
    }
    void UpdateAttack()
    { 
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            OnAttackPressed();
        }

    }

    void UpdateDirection()
    {
        Vector2 newDirection = new Vector2(
        Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical"));

        float hold = 0.4f;

        if (Mathf.Abs(newDirection.x) < hold)
        {
            newDirection.x = 0;
        }
        else
        {
            newDirection.x = Mathf.Sign(newDirection.x);
        }

        if (Mathf.Abs(newDirection.y) < hold)
        {
            newDirection.y = 0;
        }
        else
        {
            newDirection.y = Mathf.Sign(newDirection.y);
        }
       

        SetDirection(newDirection);
    }
}
