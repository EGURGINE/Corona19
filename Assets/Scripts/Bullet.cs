using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    public float speed;
    // Start is called before the first frame update

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.forward*speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;
        }
    }
}
