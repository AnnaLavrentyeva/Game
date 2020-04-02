using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHelper : MonoBehaviour
{
    public GameObject EnterToThePacman;
    public CharacterMovementModel movementModel;


    private void Awake()
    {
        EnterToThePacman.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject.Find("Values").GetComponent<CharacterValues>().UpdateRps();


        if (collision.tag == "Player")
        {
            EnterToThePacman.SetActive(true);
            movementModel.SetFrozen(true, true, true);
        }
    }
}
