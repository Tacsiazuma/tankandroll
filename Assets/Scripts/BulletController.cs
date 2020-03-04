using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D self;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject wallHit;

    [SerializeField]
    private GameObject brickHit;

    [SerializeField]
    private UnityEvent onHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            var hit = Instantiate(brickHit, gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(hit, .1f);

        } else if (collision.gameObject.CompareTag("Wall"))
        {
            var hit = Instantiate(wallHit, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(hit, .1f);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            var hit = Instantiate(wallHit, gameObject.transform.position, Quaternion.identity);
            onHit.Invoke();
            Destroy(gameObject);
            Destroy(hit, .1f);


        }
    }

}
