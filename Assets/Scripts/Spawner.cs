using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] bloods;
    [SerializeField] Transform[] pos;

    public float Cnt,maxCnt;
    private void Update()
    {
        if (Cnt>=maxCnt)
        {
            Instantiate(bloods[Random.Range(0,3)], pos[Random.Range(0,5)]);
            Cnt = 0;
        }
        Cnt += Time.deltaTime;
    }
}
