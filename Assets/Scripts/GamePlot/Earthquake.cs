using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
        void Start()
    {
        StartCoroutine( WhatForTwoSecondsBeforeStart());
    }


    IEnumerator WhatForTwoSecondsBeforeStart()
    {
        yield return new WaitForSeconds(3.1f);

        GameObject.Find("Values").GetComponent<CharacterValues>().SetBoolFromPlot();
        Application.LoadLevel("World");

    }
}
