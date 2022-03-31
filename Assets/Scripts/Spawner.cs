using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] bloods;
    public Transform[] pos;

    public float Cnt,maxCnt;
    private void Update()
    {
        if (Cnt>=maxCnt)
        {
            int ran = Random.Range(1, 10);
            switch (ran)
            {
                case 1:
                    Instantiate(bloods[0], pos[Random.Range(0, 5)]);
                    break;
                case 2:
                    Instantiate(bloods[0], pos[Random.Range(0, 5)]);
                    break;
                case 3:
                    Instantiate(bloods[1], pos[Random.Range(0, 5)]);
                    break;
                case 4:
                    Instantiate(bloods[1], pos[Random.Range(0, 5)]);
                    break;
                case 5:
                    Instantiate(bloods[1], pos[Random.Range(0, 5)]);
                    break;
                case 6:
                    Instantiate(bloods[1], pos[Random.Range(0, 5)]);
                    break;
                case 7:
                    Instantiate(bloods[1], pos[Random.Range(0, 5)]);
                    break;

                default:
                    Instantiate(bloods[2], pos[Random.Range(0, 5)]);
                    break;
            }
            Cnt = 0;
        }
        Cnt += Time.deltaTime;
    }
}
