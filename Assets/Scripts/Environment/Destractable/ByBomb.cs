using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByBomb : MonoBehaviour
{
    public virtual void DestroyByBomb()
    {
        Destroy(gameObject);
    }
}
