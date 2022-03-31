using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public Text RankNameCurrent;
    public Text RankScoreCurrent;

    public float[] rankScore;
    public Text[] rankScoreText;

    public string[] rankName;
    public Text[] rankNameText;

    private void Start()
    {
        RankNameCurrent.text = PlayerPrefs.GetString("CurrentPlayerName");
        RankScoreCurrent.text =
            string.Format("{0}",PlayerPrefs.GetFloat("CurrentPlayerScore"));
        for (int i = 0; i < 5; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            rankScoreText[i].text = string.Format("{0}", rankScore[i]);
            rankName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");
            rankNameText[i].text = string.Format(rankName[i]);
        }
    }
}
