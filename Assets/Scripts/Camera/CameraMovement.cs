using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    AudioSource source;
    public AudioClip mainBG;

    public static CameraMovement Instance;
    public RoomParent currentRoom;
    public ForestRoomParent1 forestCurrentRoom;

    public float cameraSpeed;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        Instance = this;
    }

    void Start()
    {
        source.clip = mainBG;
        source.Play();
    }

    private void Update()
    {
        UpdatePosition();
        UpdateForestPosition();
    }

    void UpdatePosition()
    {
        if (currentRoom == null)
        {
            return;
        }
        Vector3 targetPosition = currentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                                                    Time.deltaTime * cameraSpeed);
    }

    void UpdateForestPosition()
    {
        if (forestCurrentRoom == null)
        {
            return;
        }
        Vector3 targetPosition = forestCurrentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                                                    Time.deltaTime * cameraSpeed);
    }
}
