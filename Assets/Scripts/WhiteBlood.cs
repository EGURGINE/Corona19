using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlood : MonoBehaviour
{
    [SerializeField] private float Hp;
    [SerializeField] private GameObject[] items;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.back*10;
    }

        // Update is called once per frame
        void Update()
    {
        if (Hp<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(items[Random.Range(0, 6)]
        ,GameObject.Find("Spawner").GetComponent<Spawner>().pos[Random.Range(0, 5)].transform.position
        , Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Hp -= other.GetComponent<Bullet>().dmg;
        }
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }
}
