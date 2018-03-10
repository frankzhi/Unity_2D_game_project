using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardPlayer : MonoBehaviour {

    public Transform target;
    public float moveSpeed = 2.0f;
    public float maxdistance =10.0f;
    public float mindistance = 0.5f;
    public float distance;
    public float VelX;
    public float velY;
    public bool facingRight = true;
    Rigidbody2D rb;

    private Transform EnemyTransform;

    void Awake()
    {
        EnemyTransform = transform;
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	

	void Update () {

        distance = Vector3.Distance(target.position, EnemyTransform.position);

        if (distance < maxdistance && distance > mindistance)
        {
            // Get a direction vector from enemy to the target
            Vector3 dir = target.position - EnemyTransform.position;

            // Normalize it so that it's a unit direction vector
            dir.Normalize();

            // Move ourselves in that direction
            EnemyTransform.position += dir * moveSpeed * Time.deltaTime;

            Debug.Log("Player's position: " + (target.position.x - EnemyTransform.position.x));
        }
    }

    void LateUpdate()
    {
        Vector2 localScale = EnemyTransform.localScale;
        float direction = target.position.x - EnemyTransform.position.x;

        if (direction < 0 && !facingRight || direction > 0 && facingRight)
            {
                facingRight = !facingRight;
                localScale.x *= -1;
            //= new Vector2(EnemyTransform.localScale.x * -1, EnemyTransform.localScale.y);
        }
        EnemyTransform.localScale = localScale;
    }
}
