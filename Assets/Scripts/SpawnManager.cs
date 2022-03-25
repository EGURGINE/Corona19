using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject bacteria;
    [SerializeField] private GameObject virus;
    [SerializeField] private GameObject cancer;
    [SerializeField] private GameObject germ;
    
    [SerializeField] List<Transform> spawnPoints;
    private List<EnemyData> enemyDatas = new List<EnemyData>();

    private int dataIdx;
    private float spawnTime;
    private bool isReadEnemyData;
    private bool isEndEnemyData;

    private void Start()
    {
        dataIdx = 0;
    }

    public void ReadEnemyData(string textFileTExt)
    {
        enemyDatas.Clear();

        var stringReader = new StringReader(textFileTExt);
        Debug.Assert(stringReader != null);

        while (true)
        {
            string lineTExt = stringReader.ReadLine();
            if (lineTExt == null) break;

            string[] splitText = lineTExt.Split(',');

            var enemyData = new EnemyData();
            enemyData.spawnDelay = float.Parse(splitText[0]);
            enemyData.enemyType = splitText[1];
            enemyData.pointNum = int.Parse(splitText[2]);

            enemyDatas.Add(enemyData);
        }

        stringReader.Close();

        spawnTime = Time.time + enemyDatas[dataIdx].spawnDelay;
        isReadEnemyData = true;
    }

    private void Update()
    {
        if (Time.time >= spawnTime && isReadEnemyData && isEndEnemyData == false && GameManager.Instance.isStopSpawn==false)
        {
            SpawnEnemy();
            dataIdx++;
            if (dataIdx == enemyDatas.Count)
            {
                isEndEnemyData = true;
                return;
            }

            spawnTime = Time.time + enemyDatas[dataIdx].spawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoints[enemyDatas[dataIdx].pointNum - 1].position;

        switch (enemyDatas[dataIdx].enemyType)
        {
            case "bacteria": Instantiate(bacteria, spawnPos, bacteria.transform.rotation); break;
            case "virus": Instantiate(virus, spawnPos, virus.transform.rotation); break;
            case "cancer": Instantiate(cancer, spawnPos, cancer.transform.rotation); break;
            case "germ": Instantiate(germ, spawnPos, germ.transform.rotation); break;
            default: Debug.Assert(false); break;
        }
    }
}
