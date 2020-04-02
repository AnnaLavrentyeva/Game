using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDungeon : MonoBehaviour
{
    CharacterValues cv;
    bool getSword;

    private void Awake()
    {
        cv = GetComponent<CharacterValues>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject.Find("Values").GetComponent<CharacterValues>().SetBoolFirstDungeon();

        if (collision.CompareTag("Player"))
        {
            Application.LoadLevel("Game");
        }
    }

    
}
