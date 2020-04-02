using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static void SetSortingLayerToSprites(Transform parent, string sortingLayerName)
    {
        SpriteRenderer[] renderers = parent.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sprite in renderers)
        {
            sprite.sortingLayerName = sortingLayerName;
        }
    }
}
