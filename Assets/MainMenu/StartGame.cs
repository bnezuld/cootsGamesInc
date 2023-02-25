using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TMP_Text text;
    public string[] texts;
    private int textCount;

    public void LoadScene(string sceneName)
    {
        if(texts.Length <= textCount)
        {
            gameMonitor.lives = 3;
            SceneManager.LoadScene(sceneName);
        }else
        {
            text.text = texts[textCount++];
        }
    }

    public void Exit()
    {        
       Application.Quit(); 
    }
}
