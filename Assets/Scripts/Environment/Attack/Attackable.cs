using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public virtual void Hit(Collider2D hitCollider, ItemType itemType)
    {
        Debug.LogWarning("No Hit method was wet up" + gameObject.name, gameObject);
    }
}
