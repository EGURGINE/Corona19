using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public Slider HpSlider, SickSlider;
    public float MaxHp, CurHp;
    public float MaxSick, CurSick;

    public Text Score;
    public float ScoreValue;

    public float PlayerDamage;

    public bool IsInvincibility;

    public bool IsLazer;

    public GameObject Lazers;

    //public int MaxEmmoIdx;
    //public int IsEmmoIdx;
    public ParticleSystem[] LazerParticles;
    [SerializeField] private int stageNum;
    [SerializeField] private SpawnManager spawnManager;

    private void Awake()
    {
        Instance = this;
        stageNum = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurSick = stageNum == 1 ? 10 : 30;

        if (stageNum == 1)
        {
            Score.text = "0";
        }
        ReadEnemyData(stageNum);
    }

    private void FixedUpdate()
    {
        ScoreText();
        Lazers.transform.position = GameObject.Find("Player").transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        SliderManager();
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

    public void Items(int num)
    {
        switch (num)
        {
            case 1:
                //MaxEmmoIdx += 5;
                PlayerDamage++;
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
                LayzerItem();
                break;
            case 6:
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
        LazerParticles[0].Play();
        LazerParticles[1].Play();
        LazerParticles[2].Play();
        IsLazer = true;
        yield return new WaitForSeconds(3f);
        IsLazer = false;
    }

    private void ReadEnemyData(int stageNum)
    {
        spawnManager.ReadEnemyData(
            Resources.Load<TextAsset>($"Stage{stageNum}_EnemyData").text);
    }

    public void GameOver()
    {
        // ·©Å·Ã¢ ¶ç¿ì±â
    }

}