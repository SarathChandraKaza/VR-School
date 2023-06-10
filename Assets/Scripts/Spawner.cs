
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab = null;
    public float spawnTime = 5.0f;
    private float nextSpawn = 0f;
    void Start()
    {
        nextSpawn = 0f;
    }
    void Update()
    {
        nextSpawn += Time.deltaTime;

        if (nextSpawn > spawnTime)
        {
            GameObject projectileGameObject = Instantiate(spawnPrefab, transform.position, transform.rotation, null);
            nextSpawn = 0f;
        }

    }
}
