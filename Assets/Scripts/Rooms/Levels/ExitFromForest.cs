using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromForest : MonoBehaviour
{
    public Character character;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Application.LoadLevel("World");


        }
    }
}
