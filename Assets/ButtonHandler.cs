using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandler : GameWinCondition
{
       
    public GameObject leftHand;
    public GameObject rightHand;

    public Vector3 leftHandDesitination;
    public Vector3 RightHandDesitination;

    public float timerForLose;
    public float timerRemaining;

    public Sprite redDepressed;
    public Sprite redPressed;
    public Sprite whiteDepressed;
    public Sprite whitePressed;

    public bool Pressed = false;
    public bool active = false;

    private SpriteRenderer spriteRend;

    public TMP_Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        leftHandDesitination = leftHand.transform.position;
        RightHandDesitination = rightHand.transform.position;

        timerRemaining = Random.Range(5,7);

        spriteRend = GetComponent<SpriteRenderer>();

        Pressed = false;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0,0,0);
        Vector3 velocity2 = new Vector3(0,0,0);
        leftHand.transform.position = Vector3.SmoothDamp(leftHand.transform.position, leftHandDesitination, ref velocity, .01f);
        rightHand.transform.position = Vector3.SmoothDamp(rightHand.transform.position, RightHandDesitination, ref velocity2, .01f);
        if(!stop)
        {

            timerRemaining -= Time.deltaTime;
            if(timerRemaining > 0)
            {
                if(Pressed)
                {
                    spriteRend.sprite = whitePressed;
                }else
                {
                    spriteRend.sprite = whiteDepressed;
                }
                float shakeAmount = .2f;
                // leftHandDesitination = leftHandDesitination + new Vector3(Random.Range(-shakeAmount, shakeAmount),0,0);
                // RightHandDesitination = RightHandDesitination + new Vector3(Random.Range(-shakeAmount, shakeAmount),0,0);
            }else
            {      
                active = true;
                if(Pressed)
                {
                    spriteRend.sprite = redPressed;
                }else
                {
                    spriteRend.sprite = redDepressed;
                }      
                timerForLose -=  Time.deltaTime;
                if(timerForLose < 0)
                {
                    spriteRend.sprite = redPressed;
                    gameText.text = "<color=#ff0000>Rejected";
                    RightHandDesitination = new Vector3(4,rightHand.transform.position.y,rightHand.transform.position.z);
                    lose = true;
                }
            }
        }
    }

    void OnMouseDown() 
    {
        if(!stop && !lose)
        {
            Pressed = true;
            if(active)
            {
                spriteRend.sprite = redPressed;
                win = true;
                gameText.text = "<color=#00ff00>Reject";
                leftHandDesitination = new Vector3(-3.3f,leftHand.transform.position.y,leftHand.transform.position.z);
            }else
            {
                spriteRend.sprite = whitePressed;
                lose = true;
                gameText.text = "<color=#ff0000>ummm...";
                leftHandDesitination = new Vector3(-3.3f,leftHand.transform.position.y,leftHand.transform.position.z);
            }
        }
    }

    void OnMouseUp() 
    {
        if(!stop)
        {
            Pressed = false;
            if(active)
            {
                spriteRend.sprite = redDepressed;
            }else
            {
                spriteRend.sprite = whiteDepressed;
            }
        }
    }
}
