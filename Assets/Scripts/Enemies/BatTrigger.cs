using UnityEngine;
using System.Collections;

public class BatTrigger : MonoBehaviour
{
    CharacterBatControl control;

    void Awake()
    {
        control = GetComponentInParent<CharacterBatControl>();
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            control.SetCharacterInRange(null);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            control.SetCharacterInRange(collider.gameObject);
        }
    }
}
