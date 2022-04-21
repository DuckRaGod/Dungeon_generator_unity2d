using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dungeon : MonoBehaviour{
    public int number_of_rooms_to_create;
    public RoomPallate room_pallate;
    public float room_width,room_height;
    List<Vector2Int> rooms = new();
    List<Vector2Int> rooms_slot = new();

    public void Start(){
        GenerateDungeon();    
    }

    public void GenerateDungeon(){
        AddRoom(0,0);                                       //  Spawn start room!
        
        CreateRooms();
        SpawnRoomFinal();
    }

    void CreateRooms(){
        var num = rooms_slot.Count;                         //  Number of possible rooms
        while(number_of_rooms_to_create > 0){               //  Will loop unitl spawned needed amount of rooms!
            for (int i = 0; i < num; i++){                  //  Checking every possible room to spawn random spawn!
                if(number_of_rooms_to_create <= 0) return;  //  Check that number of room that spawned is not more then needed!
                                                            //! Here need to be the fourmula that give the chance to spawn room!
                                                            //? Maybe add forking rooms checking if room to spawn have x amount of neighbours
                                                            //  If have 2 rooms to spawn and have only two slots to spawn rooms then chance to spawn is 100%!
                                                            //  And if have 1 room to spawn and 2 slots then chance is 50%!
                var roomPos = rooms_slot[i];                //  Position of possible room!
                AddRoom(roomPos.x,roomPos.y);               //  Adding possible room to rooms
            }
            num = rooms_slot.Count;                         //  Updating the number of possible rooms amount
        }
    }

    void SpawnRoomFinal(){
        var amount_of_rooms = rooms.Count;
        for (int i = 0; i < amount_of_rooms; i++){
            var prefab = RoomToSpawn(i);
            var obj = Instantiate(prefab);
            obj.transform.position = new Vector3(rooms[i].x * room_width, rooms[i].y * room_height , 0);
        }
    }

    GameObject RoomToSpawn(int i){
        var doors = CheckNeigbours(rooms[i]);
        if(doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true){
            return room_pallate.Right_Up_Left_Down;
        }

        else if(doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true){
            return room_pallate.Up_Left_Down;
        }
        else if(doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true){
            return room_pallate.Left_Down_Right;
        }
        else if(doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true){
            return room_pallate.Up_Right_Down;
        }
        else if(doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false){
            return room_pallate.Left_Up_Right;
        }


        else if(doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == false){
            return room_pallate.Up_Right;
        }
        else if(doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true){
            return room_pallate.Up_Left;
        }
        else if(doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == false){
            return room_pallate.Left_Down;
        }
        else if(doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true){
            return room_pallate.Down_Right;
        }
        else if(doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false){
            return room_pallate.Left_Right;
        }
        else if(doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true){
            return room_pallate.Up_Down;
        }
        
        else if(doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false){
            return room_pallate.Right;
        }
        else if(doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false){
            return room_pallate.Up;
        }
        else if(doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false){
            return room_pallate.Left;
        }
        else if(doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true){
            return room_pallate.Down;
        }

        return null;
    }

    bool[] CheckNeigbours(Vector2Int room_pos){
        bool[] door = new bool[4];                          // 0 right, 1 up , 2 left , 3 down

        if(rooms.Contains(room_pos + new Vector2Int(1,0))){ //   Right room exist!
            door[0] = true;
        }else door[0] = false;

        if(rooms.Contains(room_pos + new Vector2Int(0,1))){ //   Up room exist!
            door[1] = true;
        }else door[1] = false;

        if(rooms.Contains(room_pos + new Vector2Int(-1,0))){ //   Left room exist!
            door[2] = true;
        }else door[2] = false;

        if(rooms.Contains(room_pos + new Vector2Int(0,-1))){ //   Down room exist!
            door[3] = true;
        }else door[3] = false;

        return door;
    }

    void AddRoom(int x ,int y){
        var pos = new Vector2Int(x,y);
        if(rooms.Contains(pos)) return;                     //  Have already this room!
        rooms_slot.Remove(pos);                             //  If this room is in rooms slot then remove from there!   
        rooms.Add(new Vector2Int(x,y));                     //  Add room to rooms list!
        number_of_rooms_to_create--;                        //  Decrease by one number_of_rooms_to_create becouse we created one room!

        Debug.Log($"Succesfuly spawned room in {pos}");                              
        AddPosToPossibleRoom(x+1,y);                        //  Adds pos to rooms slot list
        AddPosToPossibleRoom(x-1,y);
        AddPosToPossibleRoom(x,y+1);
        AddPosToPossibleRoom(x,y-1);
    }

    void AddPosToPossibleRoom(int x ,int y){
        var pos = new Vector2Int(x,y);
        if(rooms_slot.Contains(pos) || rooms.Contains(pos)) return;
        rooms_slot.Add(pos);
    }
}
