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

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        ScoreText();
    }
    // Update is called once per frame
    void Update()
    {
        BarManager();
    }
    void BarManager()
    {
        HpSlider.value = CurHp / MaxHp;
        SickSlider.value = CurSick / MaxSick;
    }
    void ScoreText()
    {
        float sc = (float)(Math.Truncate(ScoreValue) / 1);
        Score.text = "Score : " + sc;
    }
}
