using System.Collections;
using UnityEngine;
[System.Serializable]

public struct MoveRange
{
    public float xMin;
    public float XMax;
    public float ZMin;
    public float ZMax;
}

public class Player : MonoBehaviour
{
    private const int ROTATION_SPD = 80;

    [Header("플레이어 속성")]
    public int atkLevel;
    public float spd;
    public float atkDmg;
    [SerializeField] private float bulletSpd;
    [SerializeField] private float bulletInterval;
    [SerializeField] private Transform firePos;

    [Space(20f)]
    [SerializeField] private Bullet bulletObj;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject hitShield;

    [Header("움직일 수 있는 반경")]
    [SerializeField] private MoveRange moveRange;

    Rigidbody rigid;

    private float bulletTime;
    public float rotationZ;
    public bool isAttacked;

    [Header("발사 속도")]
    public GameObject emmo;
    public float shooTime, delayTime;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        hitShield.SetActive(false);

    }
    private void FixedUpdate()
    {
        #region 발사
         if (Input.GetKey(KeyCode.Space) && GameManager.Instance.IsLazer == false && Time.time > bulletTime/*&&GameManager.Instance.IsEmmoIdx<GameManager.Instance.MaxEmmoIdx*/)
        {
            bulletTime = Time.time + bulletInterval;
            Bullet bullet = Instantiate(bulletObj, firePos.position, bulletObj.transform.rotation);
            bullet.SetBullet(atkDmg, Vector3.forward, bulletSpd);
        }
        #endregion
        #region 회전
        float x = Input.GetAxis("Horizontal");

        if (Mathf.Approximately(x, 0))
        {
            rotationZ = Mathf.Lerp(rotationZ, 0, Time.deltaTime * 2f);
        }
        else
        {
            rotationZ += -x * ROTATION_SPD * Time.deltaTime;
            rotationZ = Mathf.Clamp(rotationZ, -50, 50);
        }

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            rotationZ);
        #endregion
        #region 이동
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        rigid.velocity = moveDir * (Input.GetKey(KeyCode.LeftShift) ? spd / 2 : spd);
        rigid.position = new Vector3(
            Mathf.Clamp(rigid.position.x, moveRange.xMin, moveRange.XMax),
            10,
            Mathf.Clamp(rigid.position.z, moveRange.ZMin, moveRange.ZMax)
            );

        #endregion
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacked) return;

        if (other.CompareTag("EnemyBullet"))
        {
            //SoundManager.Instance.PlaySound(Sound_Effect.HIT);
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            GameManager.Instance.CurHp = Mathf.Max(0, GameManager.Instance.CurHp - other.GetComponent<Bullet>().dmg);
            if (Mathf.Approximately(GameManager.Instance.CurHp, 0))
            {
                OnDie();
            }
            else
            {
                OnHit();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Instantiate(hitEffect).transform.position = other.transform.position;

            GameManager.Instance.CurHp = Mathf.Max(0, GameManager.Instance.CurHp - other.GetComponent<Enemy>().atkDmag/2f);
            if (Mathf.Approximately(GameManager.Instance.CurHp, 0))
            {
                OnDie();
            }
            else
            {
                OnHit();
            }
        }
    }
    private void OnHit()
    {
        StartCoroutine(HitCoroutine());
    }
    private void OnDie()
    {
        GameManager.Instance.GameOver();
    }

    public IEnumerator Invincibility()
    {
        isAttacked = true;
        hitShield.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        isAttacked = false;
    }

    private IEnumerator HitCoroutine()
    {
        //MainCamera.Instance.DamagedShake();

        isAttacked = true;
        hitShield.SetActive(true);
        yield return new WaitForSeconds(1f);

        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitShield.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        isAttacked = false;
    }
}
