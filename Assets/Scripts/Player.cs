using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public Rigidbody2D rb;
    public float speed;
    Vector2 dir => new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

    void FixedUpdate(){
        rb.velocity = dir * speed;
    }
}
