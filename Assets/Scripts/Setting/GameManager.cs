using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("플레이어 속성")]
    public Slider HpSlider, SickSlider;
    public float MaxHp, CurHp;
    public float MaxSick, CurSick;

    public bool IsInvincibility;


    [Header("점수")]
    public Text Score;
    public float ScoreValue;
    [SerializeField] GameObject InputFild;
    private float[] bestScore = new float[5];
    private string[] bestName = new string[5];

    [Header("아이템 속성")]
    //public int MaxEmmoIdx;
    //public int IsEmmoIdx;
    public bool IsLazer;

    public GameObject Lazers;
    public ParticleSystem[] LazerParticles;
    public int stageNum;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject Pets;

    [Header("보스")]
    [SerializeField] private GameObject Warring;
    [SerializeField] private GameObject[] Boss;
    [SerializeField] private Transform BossPos;
    [SerializeField] private GameObject StageText;
    public float BossSpawnScore;
    public bool isBoos;
    public bool isStopSpawn;

    private void Awake()
    {
        Instance = this;
        InputFild.SetActive(false);
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
        else if (stageNum==2)
        {
            ScoreValue = PlayerPrefs.GetFloat("Stage_1Value");
        }
        ReadEnemyData(stageNum);
        Lazers.SetActive(false);
        StartCoroutine(StageTextCnt());
    }

    private void FixedUpdate()
    {
        if (GameObject.Find("Player"))
        {
            Lazers.transform.position = GameObject.Find("Player").transform.position;
        }
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
        if (stageNum == 1 && isBoos == false)
        {
            bossStage();
        }
    }
    public Transform asedff;

    IEnumerator StageTextCnt()
    {
        isStopSpawn = true;
        StageText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StageText.SetActive(true);
        yield return new WaitForSeconds(1f);
        StageText.SetActive(false);
        isStopSpawn = false;

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
        if (ScoreValue >= BossSpawnScore)
        {
            StartCoroutine(BossWarring());
            //Debug.Log("보스 소환");
            //Instantiate(Boss[stageNum - 1],BossPos.position,Boss[stageNum-1].transform.rotation);
            isBoos = true;
            isStopSpawn = true;
        }
    }
    IEnumerator BossWarring()
    {
        Warring.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Warring.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Warring.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Warring.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Warring.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Warring.SetActive(false); Debug.Log("보스 소환");
        Instantiate(Boss[stageNum - 1], BossPos.position, Boss[stageNum - 1].transform.rotation);
    }

    public void ScoreSet(float currentScore,string currentName)
    {
        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);
        float tmpScore = 0f;
        string tmpName = "";

        for (int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            while (bestScore[i] < currentScore)
            {
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;

                if (bestScore[i]==0)
                {
                    bestScore[i] = 0;
                }
                if (bestName[i] == null)
                {
                    bestName[i] = "Null";
                }

                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                currentScore = tmpScore;
                currentName = tmpName;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);

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
                StartCoroutine(GameObject.Find("Player").GetComponent<Player>().Invincibility());
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
        // 랭킹창 띄우기
        InputFild.SetActive(true);
    }
}