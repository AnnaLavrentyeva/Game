using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLobsterControl : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public bool turnRight = true;


    void Update()
    {
        if (speed > 0)
        {
            if (turnRight)
            {
//                transform.localScale = new Vector2(-1, 1);
                transform.Translate( 2 * speed * Time.deltaTime, 0, 0);
            }
            else
            {
  //              transform.localScale = new Vector2(1, 1);
                transform.Translate(-2 * speed * Time.deltaTime, 0, 0);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turn"))
        {

            if (turnRight)
            {
                turnRight = false;
            }
            else
            {
                turnRight = true;
            }
        }
    }



}
