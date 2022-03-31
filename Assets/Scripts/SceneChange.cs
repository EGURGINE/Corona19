using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
public class SceneChange : MonoBehaviour
{
    [SerializeField] private PlayableDirector FirstMove;
    [SerializeField] private PlayableDirector SecondMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);
        FirstMove.Stop();
        SecondMove.Play();
    }

    public void MoveInGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void RankBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);
        SceneManager.LoadScene("RanKing");
    }
    public void MainBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);
        SceneManager.LoadScene("Title");
    }

    public void ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
