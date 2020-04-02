using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPacman : MonoBehaviour
{
    public Text pubScore;
    private int num;

    void Update()
    {
       // num = GameObject.Find("Game").GetComponent<GameBoard>().score;
      //  pubScore.text = Mathf.RoundToInt(num).ToString();
    }
}
