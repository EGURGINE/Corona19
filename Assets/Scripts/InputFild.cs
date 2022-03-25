using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InputFild : MonoBehaviour
{
    public InputField playerNameInput;
    private string playerName = null;
    StreamWriter sw;

    private void Awake()
    {
        playerName = playerNameInput.GetComponent<InputField>().text;
    }

    private void Update()
    {
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    public void InputName()
    {
        string fullpth = "Assets/Resources/Ranking";
        sw = new StreamWriter(fullpth);        
        playerName = playerNameInput.text;
        sw.WriteLine(playerName);
    }
}
