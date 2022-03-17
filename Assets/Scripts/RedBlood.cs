using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlood : MonoBehaviour
{
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp<=0)
        {
            GameManager.Instance.CurSick += 10;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp -= GameManager.Instance.PlayerDamage;
        }
    }
}
