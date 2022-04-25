using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour{
    public int numberOfRooms;
    public GameObject prefab;

    void Start(){
        rooms.Add(Vector2Int.zero);
        AddRoomPoints(Vector2Int.zero);
        Instantiate(prefab, Vector3.zero , Quaternion.identity);
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            Cycle();
        }
    }

    List<Vector2Int> roomPoints = new();
    List<Vector2Int> rooms = new();
    List<int> removePointIndex = new();
    float pracantgeToSpawn = .5f;

    int NeigboursRoomAmount(Vector2Int room){
        // !Todo Check how much rooms is neighbour to the room and return the amount (1 - 8)
        var amount = 0;
        if(rooms.Contains(room + new Vector2Int(1,0))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(0,1))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(-1,0))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(0,-1))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(1,1))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(-1,-1))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(1,-1))){
            amount++;
        }
        if(rooms.Contains(room + new Vector2Int(-1,1))){
            amount++;
        }
        return amount;
    }
    float ChanceRoomSpawn(int neigbourRoomAmount){
        // !Todo amount of rooms and neighbour room amount change the chance of spawn room 
        // The lower number of neigbour rooms the forky the doungen be

        if(neigbourRoomAmount > 3) return 0; 
        return Random.Range(0f,1f);  // 0 - 1 ->  0% - 100%
    }

    void AddRoom(Vector2Int room){
        if(rooms.Contains(room)) return;
        removePointIndex.Add(roomPoints.IndexOf(room));
        var chanceToSpawn = ChanceRoomSpawn(NeigboursRoomAmount(room));
        if(Random.value >= chanceToSpawn) return;
        rooms.Add(room);
        AddRoomPoints(room);
        Instantiate(prefab, new Vector3(room.x,room.y,0) , Quaternion.identity);
    }

    void AddRoomPoints(Vector2Int roomPoint){
        if(!roomPoints.Contains(roomPoint + new Vector2Int(1, 0)) && !rooms.Contains(roomPoint + new Vector2Int(1, 0))){
            roomPoints.Add(roomPoint + new Vector2Int(1, 0));
        }
        if(!roomPoints.Contains(roomPoint + new Vector2Int(-1, 0)) && !rooms.Contains(roomPoint + new Vector2Int(-1, 0))){
            roomPoints.Add(roomPoint + new Vector2Int(-1, 0));
        }
        if(!roomPoints.Contains(roomPoint + new Vector2Int(0, 1)) && !rooms.Contains(roomPoint + new Vector2Int(0, 1))){
            roomPoints.Add(roomPoint + new Vector2Int(0, 1));
        }
        if(!roomPoints.Contains(roomPoint + new Vector2Int(0, -1)) && !rooms.Contains(roomPoint + new Vector2Int(0, -1))){
            roomPoints.Add(roomPoint + new Vector2Int(0, -1));
        }
    }

    void ClearPoints(){
        var length = removePointIndex.Count;
        for (int i = length - 1; i > 0; i--){
            roomPoints.RemoveAt(removePointIndex[i]);
        }
        removePointIndex.Clear();
    }

    void Cycle(){
        var length = roomPoints.Count;
        for (int i = 0; i < length; i++){
            AddRoom(roomPoints[i]);
        }
        ClearPoints();
    }
}
