using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Vector3 targetDirection;
    public GameObject arrow;
    public Vector3 arrowStartDirection;

    public CharcterDetected characterDetected;

    private void Start()
    {
        arrow = Instantiate(arrowPrefab);
        arrow.SetActive(false);

        arrowStartDirection = characterDetected.transform.position;
        targetDirection = characterDetected.transform.position;
    }

    void Update()
    {
        MoveArrow();
    }

    public void MoveArrow()
    {

        arrow.transform.position = Vector3.MoveTowards(arrowStartDirection, targetDirection, 3 * Time.deltaTime);
    }



}

