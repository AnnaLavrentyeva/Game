using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeGameScene : MonoBehaviour
{
    int curRup;
    int curBmb;

    void Start()
    {
        
        curRup = GameObject.Find("Values").GetComponent<CharacterValues>().rps;
        curBmb = GameObject.Find("Values").GetComponent<CharacterValues>().bmb;
    //    Debug.Log(GameObject.Find("Values").GetComponent<CharacterValues>().rps);
    //    Debug.Log(curRup);
        GameObject.Find("Character").GetComponent<CharacterInventoryModel>().AddItem(ItemType.Rupee, curRup, PickUpType.None);
        GameObject.Find("Character").GetComponent<CharacterInventoryModel>().AddItem(ItemType.Bomb, curBmb, PickUpType.None);
    }


}
