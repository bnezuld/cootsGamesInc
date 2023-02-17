using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMonitor : MonoBehaviour
{
    public CountDownBar countDown;
    public GameWinCondition winCondit;

    public static int miniGames;
    public static int lives = 3;

    public string sceneName;

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown.timeRemaining <= 0)
        {
            countDown.timeRemaining = 6;
            lives--;
        }
        if(winCondit.win)
        {
            countDown.stop();
            SceneManager.LoadScene(sceneName);
            //show some win screen
        }
        if(winCondit.lose)
        {
            countDown.stop();
            //show some lose screen
            lives--;
        }
        if(lives <= 0)
        {
            Debug.Log("game over");
        }
    }
}
