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

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            Instantiate(emmo, GameObject.Find("Player").transform.position, Quaternion.Euler(0,0,0), GameObject.Find("Player").transform);
            shooTime = 0;
        }
        shooTime += Time.deltaTime;
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x>0)
        {
            Debug.Log("이게 왜 안되");
            anim.SetBool("IsRIght", true);
        }else if (x < 0)
        {
            anim.SetBool("IsLeft", true);
        }
        else
        {
            anim.SetBool("IsRight", false);
            anim.SetBool("IsLeft", false);
        }
        rigid.velocity = new Vector3(x * speed, 0, z * speed);
    }
}
