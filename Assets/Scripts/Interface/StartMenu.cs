using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    public void NewGame()
    {
        Application.LoadLevel("Plot");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
