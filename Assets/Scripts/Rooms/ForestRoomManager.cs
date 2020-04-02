using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDataForest
{
    public int X;
    public int Y;
    public string Name;
}

public class ForestRoomManager : MonoBehaviour
{
    public static ForestRoomManager Instance;
    string currentWorldName = "Overworld";

    RoomDataForest currentLoadRoomData;
    Queue<RoomDataForest> loadRoomQueue = new Queue<RoomDataForest>();
    List<ForestRoomParent1> loadedRooms = new List<ForestRoomParent1>();

    bool isLoadingRoom = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("End", 2, -2);
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

        if(loadedRooms.Count + loadRoomQueue.Count == 7)
        {
            name = "End";
        }
        if (loadedRooms.Count + loadRoomQueue.Count == 10)
        {
            name = "End";
        }

        if (loadedRooms.Count + loadRoomQueue.Count == 15)
        {
            name = "End";
        }

        if (loadedRooms.Count + loadRoomQueue.Count == 18)
        {
            name = "End";
        }
        if (loadedRooms.Count + loadRoomQueue.Count == 25)
        {
            name = "End";
        }



        //end of hack method

        RoomDataForest newRoomData = new RoomDataForest();
        newRoomData.Name = name;
        newRoomData.X= x;
        newRoomData.Y= y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomDataForest data)
    {
        string levelName = currentWorldName + data.Name;
      //  string levelName = data.Name;

        AsyncOperation loadedLevel =  Application.LoadLevelAdditiveAsync(levelName);

        while(loadedLevel.isDone == false)
        {
  //         Debug.Log("Loading " + levelName + " : " + Mathf.Round(loadedLevel.progress * 100) + " %");
            yield return null;
        }
    } 

    public void RegisterRoom (ForestRoomParent1 roomParent)
    {
        roomParent.transform.position = new Vector3(currentLoadRoomData.X * roomParent.Width,
                                                    currentLoadRoomData.Y * roomParent.Height, 0);

        roomParent.X = currentLoadRoomData.X;
        roomParent.Y = currentLoadRoomData.Y;

        roomParent.name = currentWorldName + " - " + currentLoadRoomData.Name + "  " + roomParent.X + ",  " + roomParent.Y;
       // roomParent.name = currentLoadRoomData.Name + "  " + roomParent.X + ",  " + roomParent.Y;

        isLoadingRoom = false;
        if(loadedRooms.Count == 0)
        {
            CameraMovement.Instance.forestCurrentRoom = roomParent;
        }
        loadedRooms.Add(roomParent);
    }

    bool DoesRoomExist(int x, int y)
    {
        return  loadedRooms.Find(item => item.X == x && item.Y == y) !=null;
    }

    string RandomRoomName()
    {
        string[] allRooms = new string[] { "Regular00", "Regular01", "Regular02", "Regular03", "Regular04" , "Regular05" , "Regular06",
                                            "Regular07", "Regular08", "Regular09", "Regular10"};
        return allRooms[Random.Range(0, allRooms.Length)]; 
    }

    public void OnPlayerEnterRoom(ForestRoomParent1 roomParent)
    {
        CameraMovement.Instance.forestCurrentRoom = roomParent;

        LoadRoom(RandomRoomName(), roomParent.X + 1, roomParent.Y);
        LoadRoom(RandomRoomName(), roomParent.X - 1, roomParent.Y);
        LoadRoom(RandomRoomName(), roomParent.X, roomParent.Y + 1);
        LoadRoom(RandomRoomName(), roomParent.X, roomParent.Y - 1);
    }
}
