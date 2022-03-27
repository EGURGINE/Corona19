using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_1 : MonoBehaviour
{
    [SerializeField] private GameObject red;
    [SerializeField] private ParticleSystem[] Pcys;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        red.SetActive(true);
        yield return new WaitForSeconds(1f);
        this.GetComponent<SphereCollider>().enabled = true;
        red.SetActive(false);
        Pcys[0].Play();
        Pcys[1].Play();
        Destroy(gameObject,2.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&other.GetComponent<Player>().isAttacked==false)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().CurHp -= 30;
        }
    }
}
