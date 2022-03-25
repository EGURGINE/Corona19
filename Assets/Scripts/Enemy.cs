using System.Collections;
using UnityEngine;

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
    public float atkDmag;


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
        Vector3 LeftPos = new Vector3 (transform.position.x - 5,transform.position.y,transform.position.z);
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

    public void OnDie()
    {
        Instantiate(dieEffect).transform.position = transform.position;
        switch (gameObject.name)
        {
            case "Bacteria(Clone)": GameManager.Instance.ScoreValue += 50; Debug.Log("Bacteria"); break;
            case "Virus(Clone)": GameManager.Instance.ScoreValue += 150; Debug.Log("Virus");  break;
            case "Cancer(Clone)": GameManager.Instance.ScoreValue += 300; Debug.Log("Cancer"); break;


            default:
                break;
        }
        Destroy(gameObject);
    }
}
