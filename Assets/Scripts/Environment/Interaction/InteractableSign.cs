using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSign : Interactable
{
    public string text;
    public override void OnInteract(Character character)
    {
        if(DialogBox.IsVisible() == true)
        {
            character.Movement.SetFrozen(false, false, true); 
            DialogBox.Hide();
        }
        else
        {
            character.Movement.SetFrozen(true, true, true);
            DialogBox.Show(text);
        } 
    }
}
