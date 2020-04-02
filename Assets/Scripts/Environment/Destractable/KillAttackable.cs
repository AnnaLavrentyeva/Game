using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAttackable : ByBomb
{
    public override void DestroyByBomb()
    {
        GetComponent<Attackable>().Hit(null, ItemType.Bomb);
    }
}
