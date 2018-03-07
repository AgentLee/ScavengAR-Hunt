using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Transform enemy;
    public Collider collider;
    public Rigidbody rb;
    public bool hit;
    public bool grounded;

    public float speed;

    public void Fall()
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {   
        if(collisionInfo.collider.tag == "Bullet") {
            hit = true;
            Fall(); 
        }
        else if(collisionInfo.collider.tag == "Ground") {
            grounded = true;
        }
    }
}