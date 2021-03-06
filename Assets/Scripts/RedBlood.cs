using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlood : MonoBehaviour
{
    public float hp;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.back*10;
    }
    void Update()
    {
        if (hp<=0)
        {
            Die();
        }
    }
    public void Die()
    {
        GameManager.Instance.CurSick += 10;
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            hp -= other.GetComponent<Bullet>().dmg;
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.CurHp += 10;
        }
    }
}
