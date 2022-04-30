using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{
    public bool isUnlocked;
    public Vector3 Destination;
    public Transform player;

    void Interact(){
        if(!isUnlocked) return; 
        player.transform.position = Destination + new Vector3(1,0,0);
    }
}
