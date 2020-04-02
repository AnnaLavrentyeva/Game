using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToWorld : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Values").GetComponent<CharacterValues>().rps =
                GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Rupee);

            GameObject.Find("Values").GetComponent<CharacterValues>().bmb =
                GameObject.Find("Character").GetComponent<CharacterInventoryModel>().GetItemCount(ItemType.Bomb);

            Debug.Log(GameObject.Find("Values").GetComponent<CharacterValues>().rps);

            GameObject.Find("Values").GetComponent<CharacterValues>().ExitFromDungeon();

            Application.LoadLevel("World");
        }
    }
}
