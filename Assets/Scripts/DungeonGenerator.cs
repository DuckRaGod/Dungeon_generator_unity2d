using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour{
    public Transform roomHolder;
    public int numberOfRooms;
    public GameObject prefab;
    public RoomPallate roomPallate;
    IEnumerator spawnDungeon;
    public Vector2 roomSize;

    void Start(){
        spawnDungeon = SpawnDungeon();
        StartCoroutine(SpawnDungeon());
    }

    IEnumerator SpawnDungeon(){
        while(true){
            var iteration = 0;
            rooms.Clear();
            roomPoints.Clear();
            removePointIndex.Clear();
            numberOfRooms = 10;
            int childs = roomHolder.childCount;
            for (int i = childs - 1; i > 0; i--){
                Destroy(roomHolder.GetChild(i).gameObject);
            }
            AddRoom(Vector2Int.zero);

            while(iteration <= numberOfRooms){
                iteration++;
                Cycle();
            }
            RightRoomSpawn();
            yield return new WaitForSeconds(1f);
        }
        yield return new();
    }
    List<Vector2Int> roomPoints = new();
    List<Vector2Int> rooms = new();
    List<int> removePointIndex = new();
    float pracantgeToSpawn = .5f;

    // Hellpers

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
        return Random.Range(.5f,1f);  // 0 - 1 ->  0% - 100%
    }

    // Functions

    void AddRoom(Vector2Int room){
        if(rooms.Contains(room)){                                            // Check if room already in list 
            removePointIndex.Add(roomPoints.IndexOf(room));                  // Add index to remove later
            return;                                     
        }
        var chanceToSpawn = ChanceRoomSpawn(NeigboursRoomAmount(room));      // Chance to spawn room
        if(Random.value >= chanceToSpawn) return;                            // Random to spawn room
        numberOfRooms--;
        rooms.Add(room);
        AddRoomPoints(room);
        removePointIndex.Add(roomPoints.IndexOf(room));                  // Add index to remove later
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

    void ClearPoints(){                                                      // Clear points where spawned room
        var length = removePointIndex.Count;
        for (int i = length - 1; i > 0; i--){
            roomPoints.RemoveAt(removePointIndex[i]);
        }
        removePointIndex.Clear();
    }

    void Cycle(){
        var length = roomPoints.Count;
        for (int i = 0; i < length; i++){
            if(numberOfRooms <= 0) return;
            AddRoom(roomPoints[i]);
        }
        ClearPoints();
    }

    void RightRoomSpawn(){
        var length = rooms.Count;
        for (int i = 0; i < length; i++){
            var prefab = RoomToSpawn(rooms[i]);

            var room = Instantiate(prefab,roomHolder);
            room.transform.position = Vec2IntToVec2(rooms[i]) * roomSize;
        }
    }

    Vector2 Vec2IntToVec2(Vector2Int vec){
        return new Vector2(vec.x,vec.y);
    }

    int[] id = new int[4];
    GameObject RoomToSpawn(Vector2Int pos){
        // U R D L
        if(rooms.Contains(pos + new Vector2Int(0, 1))){
            id[0] = 1;
        }else{
            id[0] = 0;
        }
        if(rooms.Contains(pos + new Vector2Int(1, 0))){
            id[1] = 1;
        }else{
            id[1] = 0;
        }
        if(rooms.Contains(pos + new Vector2Int(0, -1))){
            id[2] = 1;
        }else{
            id[2] = 0;
        }
        if(rooms.Contains(pos + new Vector2Int(-1, 0))){
            id[3] = 1;
        }else{
            id[3] = 0;
        }

        if(id[0] == 1 && id[1] == 1 && id[2] == 1 && id[3] == 1){
            return roomPallate.URDL[Random.Range(0,roomPallate.URDL.Length)];
        }

        else if(id[0] == 1 && id[1] == 0 && id[2] == 0 && id[3] == 0){
            return roomPallate.U[Random.Range(0,roomPallate.U.Length)];
        }
        else if(id[0] == 0 && id[1] == 1 && id[2] == 0 && id[3] == 0){
            return roomPallate.R[Random.Range(0,roomPallate.R.Length)];
        }
        else if(id[0] == 0 && id[1] == 0 && id[2] == 1 && id[3] == 0){
            return roomPallate.D[Random.Range(0,roomPallate.D.Length)];
        }
        else if(id[0] == 0 && id[1] == 0 && id[2] == 0 && id[3] == 1){
            return roomPallate.L[Random.Range(0,roomPallate.L.Length)];
        }

        else if(id[0] == 0 && id[1] == 1 && id[2] == 0 && id[3] == 1){
            return roomPallate.RL[Random.Range(0,roomPallate.RL.Length)];
        }
        else if(id[0] == 1 && id[1] == 0 && id[2] == 1 && id[3] == 0){
            return roomPallate.UD[Random.Range(0,roomPallate.UD.Length)];
        }
        else if(id[0] == 1 && id[1] == 1 && id[2] == 0 && id[3] == 0){
            return roomPallate.UR[Random.Range(0,roomPallate.UR.Length)];
        }
        else if(id[0] == 0 && id[1] == 1 && id[2] == 1 && id[3] == 0){
            return roomPallate.RD[Random.Range(0,roomPallate.RD.Length)];
        }
        else if(id[0] == 0 && id[1] == 0 && id[2] == 1 && id[3] == 1){
            return roomPallate.DL[Random.Range(0,roomPallate.DL.Length)];
        }
        else if(id[0] == 1 && id[1] == 0 && id[2] == 0 && id[3] == 1){
            return roomPallate.LU[Random.Range(0,roomPallate.LU.Length)];
        }

        else if(id[0] == 1 && id[1] == 1 && id[2] == 1 && id[3] == 0){
            return roomPallate.URD[Random.Range(0,roomPallate.URD.Length)];
        }
        else if(id[0] == 1 && id[1] == 1 && id[2] == 0 && id[3] == 1){
            return roomPallate.LUR[Random.Range(0,roomPallate.LUR.Length)];
        }
        else if(id[0] == 1 && id[1] == 0 && id[2] == 1 && id[3] == 1){
            return roomPallate.DLU[Random.Range(0,roomPallate.DLU.Length)];
        }
        else if(id[0] == 0 && id[1] == 1 && id[2] == 1 && id[3] == 1){
            return roomPallate.RDL[Random.Range(0,roomPallate.RDL.Length)];
        }
        
        Debug.LogWarning("ERROR FINDING RIGHT ROOM!");
        return null;
    }
}
