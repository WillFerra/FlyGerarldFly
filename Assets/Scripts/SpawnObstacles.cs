using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] obstacle;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        
        int random = Random.Range(0, obstacle.Length);
        GameObject instance = Instantiate(obstacle[random], transform.position + new Vector3(randomX, randomY, 0), transform.rotation);

       if (instance.CompareTag("Asteroids"))
       {
           instance.transform.position = new Vector3(instance.transform.position.x, 0);
       }
    }
}
