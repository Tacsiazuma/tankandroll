using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    private float lastSpawn = 0f;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private Transform spawnPointLeft;
    [SerializeField]
    private Transform spawnPointRight;

    [SerializeField]
    private Rigidbody2D smallTank;

    int[] enemies = new int[6] { 1, 1, 1, 1, 1, 1 };
    int spawned = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawn > spawnRate && spawned < enemies.Length - 1)
        {
            lastSpawn = Time.time;
            spawned++;
            float random = Random.Range(0f, 10f);
            if (random < 5f)
            {
                CreateEnemy(1, spawnPointLeft);
            }
            else
            {
                CreateEnemy(1, spawnPointRight);
            }
        }
    }

    private void CreateEnemy(int type, Transform transform)
    {
        switch (type)
        {
            case 1:
                transform.GetComponent<Animator>().SetBool("spawn", true);
                Instantiate(smallTank, transform.position, transform.rotation);
                break;
        }

    }
}
