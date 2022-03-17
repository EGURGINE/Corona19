using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlood : MonoBehaviour
{
    float Hp;
    GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp<=0)
        {
            Instantiate(items[Random.Range(0,6)], transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Hp -= GameManager.Instance.PlayerDamage;
        }
    }
}
