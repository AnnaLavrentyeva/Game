using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public int X;
    public int Y;
    public string Name;
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
  //  string currentWorldName = "Dungeons";

    RoomData currentLoadRoomData;
    Queue<RoomData> loadRoomQueue = new Queue<RoomData>();
    List<RoomParent> loadedRooms = new List<RoomParent>();

    bool isLoadingRoom = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadRoom("Dungeon1", 0, 0);
        LoadRoom("Exit", -2, -2);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom == true)
        {
            return; 
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

  //      Debug.Log("Loading new room " + currentLoadRoomData.X + " " + currentLoadRoomData.Y);
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y) == true) {
            return;
        }
        
 //Hack method

        if(loadedRooms.Count + loadRoomQueue.Count == 15)
        {
            name = "Exit";
        }

        if (loadedRooms.Count + loadRoomQueue.Count == 12)
        {
            name = "Exit";
        }

        if (loadedRooms.Count + loadRoomQueue.Count == 8)
        {
            name = "Exit";
        }

        if (loadedRooms.Count + loadRoomQueue.Count == 20)
        {
            name = "Exit";
        }


        //end of hack method

        RoomData newRoomData = new RoomData();
        newRoomData.Name = name;
        newRoomData.X= x;
        newRoomData.Y= y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomData data)
    {
      //  string levelName = currentWorldName + data.Name;
        string levelName = data.Name;

        AsyncOperation loadedLevel =  Application.LoadLevelAdditiveAsync(levelName);

        while(loadedLevel.isDone == false)
        {
  //         Debug.Log("Loading " + levelName + " : " + Mathf.Round(loadedLevel.progress * 100) + " %");
            yield return null;
        }
    } 

    public void RegisterRoom (RoomParent roomParent)
    {
        roomParent.transform.position = new Vector3(currentLoadRoomData.X * roomParent.Width,
                                                    currentLoadRoomData.Y * roomParent.Height, 0);

        roomParent.X = currentLoadRoomData.X;
        roomParent.Y = currentLoadRoomData.Y;

       // roomParent.name = currentWorldName + " - " + currentLoadRoomData.Name + "  " + roomParent.X + ",  " + roomParent.Y;
        roomParent.name = currentLoadRoomData.Name + "  " + roomParent.X + ",  " + roomParent.Y;

        isLoadingRoom = false;
        if(loadedRooms.Count == 0)
        {
            CameraMovement.Instance.currentRoom = roomParent;
        }
        loadedRooms.Add(roomParent);
    }

    bool DoesRoomExist(int x, int y)
    {
        return  loadedRooms.Find(item => item.X == x && item.Y == y) !=null;
    }

    string RandomRoomName()
    {
        string[] allRooms = new string[] { "Dungeon2", "Dungeon3", "Dungeon4"};
        return allRooms[Random.Range(0, allRooms.Length)]; 
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
        CameraMovement.Instance.currentRoom = roomParent;

        LoadRoom(RandomRoomName(), roomParent.X + 1, roomParent.Y);
        LoadRoom(RandomRoomName(), roomParent.X - 1, roomParent.Y);
        LoadRoom(RandomRoomName(), roomParent.X, roomParent.Y + 1);
        LoadRoom(RandomRoomName(), roomParent.X, roomParent.Y - 1);
    }
}
