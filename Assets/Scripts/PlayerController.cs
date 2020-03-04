using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D bullet;

    [SerializeField]
    private Transform spawnPoint;

    private Vector2 direction;
    private float rotation;

    [SerializeField]
    private AudioSource cannon;

    private float lastFire = 0;
    private float fireLimit = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
            rotation = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
            rotation = 180;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            rotation = 90;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            rotation = 270;
        }
        FireIfNeeded();
        if (direction != Vector2.zero)
        {
            Move();
        }

    }


    private void FireIfNeeded()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time - lastFire > fireLimit)
        {
            lastFire = Time.time;
            Rigidbody2D b = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            b.velocity = Vector2FromAngle(rotation) * 15;
            cannon.Play();
        }
    }

    public Vector2 Vector2FromAngle(float a)
    {
        a = (a + 90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    private void Move()
    {
        player.SetRotation(rotation);
        player.MovePosition(player.position + (direction * Time.fixedDeltaTime * speed));
    }

}
