using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dungeon/RoomPallate")]
public class RoomPallate : ScriptableObject{
    public GameObject Right_Up_Left_Down;  //0

    public GameObject Up_Right_Down;
    public GameObject Left_Up_Right;
    public GameObject Up_Left_Down;
    public GameObject Left_Down_Right;

    public GameObject Up_Left;
    public GameObject Up_Right;
    public GameObject Left_Down;
    public GameObject Down_Right;
    public GameObject Left_Right;
    public GameObject Up_Down;

    public GameObject Right;
    public GameObject Left;
    public GameObject Down;
    public GameObject Up;
}
// 0 right, 1 up , 2 left , 3 down