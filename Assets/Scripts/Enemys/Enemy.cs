using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("�Ϲ� ��ü �Ӽ�")]
    [SerializeField] protected float spd;
    [SerializeField] protected float hp;

    [Header("���� �Ӽ�")]
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Bullet bulletObj;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float bulletIntervar;
    public float atkDmag;

    [Header("���� ��� �Ӽ�")]
    [SerializeField] private bool isUnlimitShotcnt;

    [Header("����� ���� �Ӽ�")]
    [SerializeField] private int wayCnt;

    [Header("����Ʈ")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    [Header("�ϼ��� ���� ��ƼŬ")]
    [SerializeField] private ParticleSystem pcy;

    [Header("���� �ڽĵ�")]
    [SerializeField] private GameObject GremChild;

    [Header("����")]
    [SerializeField] protected bool IsBoss;
    [SerializeField] private GameObject Skill_1;
    [SerializeField] private GameObject BossShild;
    [SerializeField] Slider hpSlider;
    private float BossMaxHp;
    public bool bossShild;
    [SerializeField] private GameObject RanPos;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isUnlimitShotcnt) Invoke("Attack", 1f);
        else InvokeRepeating("Attack", 1f, bulletIntervar);

        if (IsBoss==false)
        {
            Move();
        }
        else
        {
            BossMaxHp = hp;
        }
    }

    private void Update()
    {
        if (IsBoss)
        {
            hpSlider.value = hp/BossMaxHp;
            hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 25, 0));
            if (GameObject.Find("BossShild(Clone)"))
            {
                bossShild = true;
            }else bossShild = false;
        }
    }

    protected abstract void Attack();
    private void Move()
    {
       rb.velocity = Vector3.back * spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bossShild) return;

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
        Debug.Log(num);

        switch (num)
        {
            case 1:
                StartCoroutine(BossSkil_1());
                break;
            case 2:
                if (bossShild)
                {
                    return;
                }
                Vector3 LeftPos = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                Vector3 RightPos = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);

                Instantiate(BossShild).transform.position = LeftPos;
                Instantiate(BossShild).transform.position = RightPos;
                break;
            default:
                firePos.LookAt(GameObject.Find("Player").transform.position);
                Bullet bullet = Instantiate(bulletObj);
                bullet.transform.position = firePos.position;
                bullet.SetBullet(atkDmag, firePos.forward, bulletSpd);
                break;
        }
    }
    IEnumerator BossSkil_1()
    {
        yield return new WaitForSeconds(1f);
        float pX = GameObject.Find("Player").transform.position.x;
        float pZ = GameObject.Find("Player").transform.position.z;
        RanPos.transform.position = new Vector3(pX += Random.Range(-6, 5), GameObject.Find("Player").transform.position.y, pZ += Random.Range(-6, 5));
        Instantiate(Skill_1, RanPos.transform);
    }
    public void OnDie()
    {
        Instantiate(dieEffect).transform.position = transform.position;
        switch (gameObject.name)
        {
            case "Bacteria(Clone)": GameManager.Instance.ScoreValue += 50; Debug.Log("Bacteria"); break;
            case "Virus(Clone)": GameManager.Instance.ScoreValue += 150; Debug.Log("Virus"); break;
            case "Cancer(Clone)": GameManager.Instance.ScoreValue += 300; Debug.Log("Cancer"); break;
            case "Boss(Clone)":
                GameManager.Instance.ScoreValue += 1500;
                GameManager.Instance.stageNum++;
                GameManager.Instance.BossSpawnScore += 25000;
                GameManager.Instance.isStopSpawn = false;
                GameManager.Instance.isBoos = false;
                break;

            default:
                break;
        }
        Destroy(gameObject);
    }
}
