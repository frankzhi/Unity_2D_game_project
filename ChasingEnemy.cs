using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class ChasingEnemy : MonoBehaviour {

    public Transform target;
    public float speed = 5.0f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb;


    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        //set the length of vector to 1
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = - rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

}
