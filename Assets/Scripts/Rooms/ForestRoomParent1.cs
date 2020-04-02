using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestRoomParent1 : MonoBehaviour
{
    public int Width;
    public int Height;

    public int X;
    public int Y;

    //float cameraSize = 12f;
    //float aspectRation = 16f / 9f;

    void Start()
    {
        ForestRoomManager.Instance.RegisterRoom(this);    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ForestRoomManager.Instance.OnPlayerEnterRoom(this);
        }    
    }

}
