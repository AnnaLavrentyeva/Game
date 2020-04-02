using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterDetected : MonoBehaviour
{
    BowmanControl control;
    Character character;
    public Arrow arrowScript;

    //public GameObject arrowPrefab;
    //Vector3 targetDirection;
    //GameObject arrow;
    //Vector3 arrowStartDirection;

    void Awake()
    {
        control = GetComponentInParent<BowmanControl>();
    }

    private void Start()
    {
        //arrow = Instantiate(arrowPrefab);
        //arrow.SetActive(false);

        //arrowStartDirection = transform.position;
        //targetDirection = transform.position;
    }

    //private void Update()
    //{
    //    MoveArrow();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            control.SetCharacter(collision.gameObject);
            arrowScript.arrow.SetActive(true);
            arrowScript.arrowStartDirection = transform.position;
            arrowScript.targetDirection = collision.gameObject.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            control.SetCharacter(null);

        }
    }
}
