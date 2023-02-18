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


    private int state;
    
    public GameObject intro;
    public float introTime;

    public GameObject retry;
    public float retryTime;

    public GameObject outro;
    public float outroTime;

    public GameObject gameOver;
    public float gameOverTime;

    public GameObject blood;    
    public GameObject hearts;

    // Start is called before the first frame update
    void Start()
    {
        if(intro != null)
            intro.SetActive(true);
        if(retry != null)
            retry.SetActive(false);
        if(outro != null)
            outro.SetActive(false);
        if(gameOver != null)
            gameOver.SetActive(false);
        stop(true);
    }

    void stop(bool stop)
    {
        winCondit.stop = stop;
        countDown.stop = stop;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case 0:
                //intro
                introTime -= Time.deltaTime;
                if(introTime <= 0)
                {
                    state = 1;
                    intro.SetActive(false);
                    stop(false);
                }
                break;
            case 1:
                //play game
                if(countDown.timeRemaining <= 0)
                {
                    countDown.timeRemaining = 6;
                    lives--;
                    state = 3;
                    stop(true);
                }
                if(winCondit.win)
                {
                    stop(true);
                    countDown.resetPitch();
                    state = 2;
                }
                if(winCondit.lose)
                {
                    stop(true);
                    //show some lose screen
                    intro.SetActive(true);
                    lives--;
                    state = 3;
                }
                if(lives <= 0)
                {
                    Debug.Log("game over");
                }
                break;
            case 2:
            //win
                hearts.SetActive(true);
                if(outro != null)
                    outro.SetActive(true);
                outroTime -= Time.deltaTime;
                if(outroTime <= 0)
                {
                    SceneManager.LoadScene(sceneName);
                }
                break;
            case 3:
            //retry
                if(retry != null)
                    retry.SetActive(true);
                blood.SetActive(true);
                retryTime -= Time.deltaTime;
                if(retryTime <= 0)
                {
                    if(lives <= 0)
                    {
                        state = 4;
                    }else
                    {
                        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
                    }
                }
                break;
            case 4:
            //gameover
                countDown.stopTick();
                if(gameOver != null)
                    gameOver.SetActive(true);
                gameOverTime -= Time.deltaTime;
                if(gameOverTime <= 0)
                {
                    Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("MainMenu");
                }
                break;
        }
    }
}
