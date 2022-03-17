using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Rigidbody rigid;
    public float speed;

    public Vector3 offset;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = Vector3.back*speed;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            this.transform.position = offset;
        }
    }
}
