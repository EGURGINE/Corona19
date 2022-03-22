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
    [SerializeField] private int shotCnt;
    [SerializeField] private float continiousShotInterval;

    [Header("����� ���� �Ӽ�")]
    [SerializeField] private int wayCnt;

    [Header("����Ʈ")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

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
    }

    public void VacteriaAttack()
    {
        StartCoroutine(spdCoroutine());
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
            case "Bacteria": GameManager.Instance.ScoreValue += 50; break;
            case "Virus": GameManager.Instance.ScoreValue += 150; break;

            default:
                break;
        }
        Destroy(gameObject);
    }
}
