using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pets : MonoBehaviour
{
    [Header("공격 속성")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 pos;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Bullet bulletObj;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float bulletInterval;
    private float bulletTime;

    private void Update()
    {
        gameObject.transform.position = player.transform.position + pos;

        if (Time.time > bulletTime)
        {
            bulletTime = Time.time + bulletInterval;
            Bullet bullet = Instantiate(bulletObj, firePos.position, bulletObj.transform.rotation);
            bullet.SetBullet(GameObject.Find("Player").GetComponent<Player>().atkDmg, firePos.forward, bulletSpd);

        }

    }
}
