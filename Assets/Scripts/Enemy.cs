using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    public float speed;

    public float MaxHp;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.back * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }
    void Die()
    {
        if (MaxHp<=0)
        {
            GameManager.Instance.ScoreValue += 100;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                GameManager.Instance.CurHp -= 10;
                Destroy(gameObject);
                break;
            case "ImSick":
                GameManager.Instance.CurSick -= 15;
                Destroy(gameObject);
                break;
            case "Bullet":
                Debug.Log("¤·¤·");
                MaxHp -= GameManager.Instance.PlayerDamage;
                break;
        }
    }
}
