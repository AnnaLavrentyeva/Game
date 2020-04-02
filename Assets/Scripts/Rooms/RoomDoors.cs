using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoors : MonoBehaviour
{
    public float DestroyProbability;

    void Start()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < DestroyProbability)
        {
            Destroy(gameObject);
        }
    }
}
