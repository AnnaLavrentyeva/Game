using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public CharacterMovementModel movementModel;

    private bool isEsc = false;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEsc = !isEsc;
        }

        if (isEsc)
        {
            PauseMenuUI.active = true;
        }

        if (!isEsc)
        {
            PauseMenuUI.active = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
