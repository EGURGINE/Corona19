using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //이동
    Rigidbody rigid;
    public float speed;
    //발사
    public GameObject emmo;
    public float shooTime, delayTime;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
        Move();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    void Fire()
    {
        if (shooTime > delayTime)
        {
            Instantiate(emmo, GameObject.Find("BulletSpawner").transform.position, Quaternion.Euler(90,0,0), GameObject.Find("BulletSpawner").transform);
            shooTime = 0;
        }
        shooTime += Time.deltaTime;
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        rigid.velocity = new Vector3(x * speed, 0, z * speed);
    }
}
