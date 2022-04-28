using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Pallate" , menuName = "Dungeon Generation")]
public class RoomPallate : ScriptableObject{
    public GameObject[] URDL;

    public GameObject[] U;
    public GameObject[] R;
    public GameObject[] D;
    public GameObject[] L;

    public GameObject[] RL;
    public GameObject[] UD;
    public GameObject[] UR;
    public GameObject[] RD;
    public GameObject[] DL;
    public GameObject[] LU;

    public GameObject[] URD;
    public GameObject[] RDL;
    public GameObject[] DLU;
    public GameObject[] LUR;
}
