using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float spd;
    [HideInInspector] public bool isScroll;

    private void Start()
    {
        isScroll = true;
    }

    private void Update()
    {
        if (isScroll)
        {
            transform.Translate(Vector3.back * spd * Time.deltaTime);
        }
    }
}
