using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    virtual public void OnInteract(Character character)
    {
        Debug.LogWarning("OnInteract is not implemented");
    }
}
