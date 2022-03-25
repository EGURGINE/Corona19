using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private const float Rank_Num = 5f;

    [Header("플레이어 속성")]
    public Slider HpSlider, SickSlider;
    public float MaxHp, CurHp;
    public float MaxSick, CurSick;

    public bool IsInvincibility;


    [Header("점수")]
    public Text Score;
    public float ScoreValue;
    public String PlayerName;

    public List<float> ScoreSet = new List<float>();
    public List<string> PlayerNameSet = new List<string>();

    [Header("아이템 속성")]
    //public int MaxEmmoIdx;
    //public int IsEmmoIdx;
    public bool IsLazer;

    public GameObject Lazers;
    public ParticleSystem[] LazerParticles;
    [SerializeField] private int stageNum;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject Pets;

    [Header("보스")]
    [SerializeField] private GameObject[] Boss;
    [SerializeField] private Transform BossPos;
    public bool isBoos;
    public bool isStopSpawn;

    private void Awake()
    {
        Instance = this;
        stageNum = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Pets.SetActive(false);

        CurSick = stageNum == 1 ? 10 : 30;

        if (stageNum == 1)
        {
            Score.text = "0";
        }
        ReadEnemyData(stageNum);
        Lazers.SetActive(false);
    }

    private void FixedUpdate()
    {
        Lazers.transform.position = GameObject.Find("Player").transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText();
        SliderManager();
        if (Input.GetKey(KeyCode.P))
        {
            Invoke("GameOver", 3f);
        }
        if (CurSick >= 100)
        {
            GameOver();
        }
        bossStage();
    }
    void ScoreText()
    {
        float sc = (float)(Math.Truncate(ScoreValue) / 1);
        Score.text = "Score : " + sc;
    }

    void SliderManager()
    {
        HpSlider.value = CurHp / MaxHp;
        SickSlider.value = CurSick / MaxSick;

    }

    public void bossStage()
    {
        if (ScoreValue >= 10000)
        {
            isBoos = true;
            Debug.Log("보스 소환");
            Instantiate(Boss[stageNum - 1],BossPos.position,Boss[stageNum-1].transform.rotation);
            isBoos = false;
            isStopSpawn = true;
        }
    }

    public void Items(int num)
    {
        switch (num)
        {
            case 1:
                GameObject.Find("Player").GetComponent<Player>().atkDmg += 50;
                break;
            case 2:
                StartCoroutine(Invincibility());
                break;
            case 3:
                CurHp += 10;
                break;
            case 4:
                CurSick -= 10;
                break;
            case 5:
                StartCoroutine(LayzerItem());
                break;
            case 6:
                StartCoroutine(PetItem());
                break;
        }
    }
    public IEnumerator Invincibility()
    {
        IsInvincibility = true;
        yield return new WaitForSeconds(3f);
        IsInvincibility = false;
    }

    IEnumerator LayzerItem()
    {
        Lazers.SetActive(true);
        LazerParticles[0].Play();
        LazerParticles[1].Play();
        LazerParticles[2].Play();
        IsLazer = true;
        yield return new WaitForSeconds(3f);
        Lazers.SetActive(false);
        IsLazer = false;
    }

    private void ReadEnemyData(int stageNum)
    {
        spawnManager.ReadEnemyData(
            Resources.Load<TextAsset>($"Stage{stageNum}_EnemyData").text);
    }

    IEnumerator PetItem()
    {
        Pets.SetActive(true);
        yield return new WaitForSeconds(5f);
        Pets.SetActive(false);
    }

    public void GameOver()
    {
        if (true)
        {

        }


        SceneManager.LoadScene(2);
        // 랭킹창 띄우기
    }

}