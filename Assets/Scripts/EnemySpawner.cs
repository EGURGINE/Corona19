using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;

    public float spawnTime, delayTime; // Äð

    public Transform[] pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
    }
    void EnemySpawn()
    {
        if (spawnTime > delayTime)
        {
            int arr= Random.Range(0, 5);
            Instantiate(enemy[0],pos[arr].position, Quaternion.Euler(90, 0, 0));
            spawnTime = 0;
        }
        spawnTime += Time.deltaTime;
    }
}
