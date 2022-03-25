using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("일반 객체 속성")]
    [SerializeField] protected float spd;
    [SerializeField] protected float hp;

    [Header("공격 속성")]
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Bullet bulletObj;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float bulletIntervar;

    [Header("연속 곡격 속성")]
    [SerializeField] private bool isUnlimitShotcnt;

    [Header("방사형 공격 속성")]
    [SerializeField] private int wayCnt;

    [Header("이펙트")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    [Header("암세포 공격 파티클")]
    [SerializeField] private ParticleSystem pcy;

    [Header("세균 자식들")]
    [SerializeField] private GameObject GremChild;
    public float atkDmag;

    [Header("보스")]
    [SerializeField] protected Bullet BossBulletObj;
    [SerializeField] protected GameObject Skill_1;
    private GameObject RanPos;


    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isUnlimitShotcnt) Invoke("Attack", 1f);
        else InvokeRepeating("Attack", 1f, bulletIntervar);

        Move();
    }

    protected abstract void Attack();

    private void Move()
    {
        rb.velocity = Vector3.back * spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            hp = Mathf.Max(0, hp - other.GetComponent<Bullet>().dmg);
            if (hp == 0)
            {
                OnDie();
            }
        }
        else if (other.CompareTag("Layzer"))
        {
            Instantiate(hitEffect).transform.position = other.transform.position;
            hp = Mathf.Max(0, hp - 500);
            if (hp == 0)
            {
                OnDie();
            }
        }
    }

    public void CancerAttack()
    {
        pcy.Play();
        GameManager.Instance.CurSick += 3;
    }
    public void VacteriaAttack()
    {
        StartCoroutine(spdCoroutine());
    }

    public void GermAttack()
    {
        Vector3 LeftPos = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        Vector3 RightPos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);

        Instantiate(GremChild).transform.position = LeftPos;
        Instantiate(GremChild).transform.position = RightPos;
    }

    public void WayShot()
    {
        firePos.LookAt(GameObject.Find("Player").transform.position);

        Quaternion firstRotation = firePos.rotation;

        firePos.Rotate(Vector3.up * -5 * (wayCnt / 2));

        for (int i = 0; i <= wayCnt; i++)
        {
            Bullet bullet = Instantiate(bulletObj);
            bullet.transform.position = firePos.position;
            bullet.SetBullet(atkDmag, firePos.forward, bulletSpd);
            firePos.Rotate(Vector3.up * 5);
        }

        firePos.rotation = firstRotation;
    }
    IEnumerator spdCoroutine()
    {
        spd += 10;
        yield return new WaitForSeconds(1f);
        spd -= 10;
    }

    public void BossAttack(int num)
    {
        switch (num)
        {
            case 1:
                firePos.LookAt(GameObject.Find("Player").transform.position);
                Bullet bullet = Instantiate(BossBulletObj);
                bullet.transform.position = firePos.position;
                bullet.SetBullet(atkDmag, firePos.forward, bulletSpd);
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    StartCoroutine(BossSkil_1());
                }
                break;
            case 3:

                break;
        }
    }

    IEnumerator BossSkil_1()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            float pX = GameObject.Find("Player").transform.position.x;
            float pZ = GameObject.Find("Player").transform.position.z;
            RanPos.transform.position = new Vector3(pX, GameObject.Find("Player").transform.position.y, pZ);
            Transform pPos = RanPos.transform;
            Instantiate(Skill_1, pPos);
        }
    }
    public void OnDie()
    {
        Instantiate(dieEffect).transform.position = transform.position;
        switch (gameObject.name)
        {
            case "Bacteria(Clone)": GameManager.Instance.ScoreValue += 50; Debug.Log("Bacteria"); break;
            case "Virus(Clone)": GameManager.Instance.ScoreValue += 150; Debug.Log("Virus"); break;
            case "Cancer(Clone)": GameManager.Instance.ScoreValue += 300; Debug.Log("Cancer"); break;


            default:
                break;
        }
        Destroy(gameObject);
    }
}
