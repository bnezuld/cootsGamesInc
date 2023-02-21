using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        gameMonitor.lives = 3;
        SceneManager.LoadScene(sceneName);
    }
}
