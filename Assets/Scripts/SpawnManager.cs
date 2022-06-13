using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] extraPrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 extraSpawnPos = new Vector3(25, 4, 0);
    private float startDelay = 2;
    private float repeatRate = 4;
    private float extraStartDelay = 4;
    private float extraRepeatRate = 4;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnExtras", extraStartDelay, extraRepeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }

    void SpawnExtras()
    {
        if (playerControllerScript.gameOver == false)
        {
            int extraIndex = Random.Range(0, extraPrefabs.Length);
            Instantiate(extraPrefabs[extraIndex], extraSpawnPos, extraPrefabs[extraIndex].transform.rotation);
        }
    }
}
