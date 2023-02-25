using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class typeCat : GameWinCondition
{

    public TMP_Text textTarget;
    public TextMeshProUGUI textRender;
    public AudioSource audio;

    public string target;
    public string remaining;

    public GameObject catSpawn;

    public Vector2 spawnTopLeft;
    public Vector2 spawnBottomRight;
    // Start is called before the first frame update
    void Start()
    {
        setTargetWord("meow meow meow meow meow meow meow meow meow meow meow");
        textRender.enabled = false;
    }

    void setRemainingWord(string word)
    {
        remaining = word;
        textTarget.text = word;
    }

    void setTargetWord(string word)
    {
        setRemainingWord(word);
    }

    // public void OnSelect (BaseEventData eventData) 
    // {
    //     isCaretPositionReset = false;
    // }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {           
            textRender.enabled = true;
            checkInput();
        }else
        {
            textRender.enabled = false;
        }
    }

    void checkInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if(keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    void EnterLetter(string letter)
    {
        if(checkLetter(letter))
        {
            removeLetter();

            if(isComplete())
            {
                // Debug.Log("playAudio");
                // audio.Play();
                // int randX = Random.Range((int)spawnTopLeft.x*10, (int)spawnBottomRight.x*10);
                // int randY = Random.Range((int)spawnTopLeft.y*10, (int)spawnBottomRight.y*10);
                // int randRotation = Random.Range(-20*10,20*10);
                // Quaternion rot = Quaternion.Euler(0, 0, randRotation/10.0f);
                // GameObject temp = Instantiate(catSpawn,new Vector3(randX/10.0f, randY/10.0f, 0), rot);
                win = true;
            }
        }
    }

    bool checkLetter(string letter)
    {
        return remaining.IndexOf(letter) == 0;
    }

    void removeLetter()
    {
        string newString = remaining.Remove(0, 1);
        setRemainingWord(newString);
        if(remaining.Length == 0 || remaining[0] == ' ')
        {
            // Debug.Log("playAudio");
            // audio.Play();
            int randX = Random.Range((int)spawnTopLeft.x*10, (int)spawnBottomRight.x*10);
            int randY = Random.Range((int)spawnTopLeft.y*10, (int)spawnBottomRight.y*10);
            int randRotation = Random.Range(-30*10,30*10);
            Quaternion rot = Quaternion.Euler(0, 0, randRotation/10.0f);
            GameObject temp = Instantiate(catSpawn,new Vector3(randX/10.0f, randY/10.0f, 0), rot);
        }
    }

    bool isComplete()
    {
        return remaining.Length == 0;
    }
}
