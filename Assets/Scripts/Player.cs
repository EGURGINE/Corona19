using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const int ROTATION_SPD = 80;


    Rigidbody rigid;
    public float speed;

    public GameObject emmo;
    public float shooTime, delayTime;

    public float rotationZ;

    Material ma;
    Animator anim;

    public Material[] MetallicMaterial;
    // Start is called before the first frame update
    void Start()
    {
        ma = GetComponent<Material>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)&&GameManager.Instance.IsLazer == false/*&&GameManager.Instance.IsEmmoIdx<GameManager.Instance.MaxEmmoIdx*/)
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
            Instantiate(emmo, GameObject.Find("Player").transform.position, Quaternion.Euler(0,0,0));
            //GameManager.Instance.IsEmmoIdx++;
            shooTime = 0;
        }
        shooTime += Time.deltaTime;
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Mathf.Approximately(x, 0))
        {
            rotationZ = Mathf.Lerp(rotationZ, 0, Time.deltaTime * 2f);
        }
        else
        {
            rotationZ += -x * ROTATION_SPD * Time.deltaTime;
            rotationZ = Mathf.Clamp(rotationZ, -50, 50);
        }

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            rotationZ);

        rigid.velocity = new Vector3(x * speed, 0, z * speed);
    }
    IEnumerator Invincibility()
    {
        GameManager.Instance.IsInvincibility = true;
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.IsInvincibility = false;
    }
}
