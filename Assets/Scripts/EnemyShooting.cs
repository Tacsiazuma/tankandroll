using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private int lives = 1;

    [SerializeField]
    private Rigidbody2D bullet;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private Rigidbody2D self;

    [SerializeField]
    private Rigidbody2D explosion;

    [SerializeField]
    private int speed;

    private float lastFire = 0;
    private float fireLimit = 2f;
    private float directionChanged = 0;
    private Vector2 direction;
    private int rotation;

    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveIfNeeded();
        FireIfNeeded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    private void MoveIfNeeded()
    {
        if (Time.time - directionChanged > UnityEngine.Random.Range(5, 10))
        {
            directionChanged = Time.time;
            direction = Vector2.zero;
            ChangeDirection();
        }
        if (direction != Vector2.zero)
        {
            Move();
        }
    }

    private void ChangeDirection()
    {
        int random = UnityEngine.Random.Range(0, 400);
        if (random < 90)
        {
            direction = Vector2.up;
            rotation = 0;
        }
        else if (random < 180)
        {
            direction = Vector2.down;
            rotation = 180;
        }
        else if (random < 270)
        {
            direction = Vector2.left;
            rotation = 90;
        }
        else if (random < 360)
        {
            direction = Vector2.right;
            rotation = 270;
        }
    }

    private void Move()
    {
        self.SetRotation(rotation);
        self.MovePosition(self.position + (direction * Time.fixedDeltaTime * speed));
    }

    private void FireIfNeeded()
    {
        if (Time.time - lastFire > fireLimit && UnityEngine.Random.Range(0, 100) > 50)
        {
            lastFire = Time.time;
            Rigidbody2D b = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Vector3 forward = transform.forward;
            forward.y = 0;
            b.velocity = Vector2FromAngle(rotation) * 15;
        }
    }

    public void Hit()
    {
        lives--;
        if (lives <= 0)
        {
            Rigidbody2D expl = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(expl, .2f);
        }
    }

    public Vector2 Vector2FromAngle(float a)
    {
        a = (a + 90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

}
