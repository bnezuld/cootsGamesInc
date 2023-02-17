using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownBar : MonoBehaviour
{

    public float totalTime = 15;
    public float timeRemaining = 10;
    public GameObject destination;
    public GameObject progress;

    private SpriteRenderer spriteRenderer;

    private int StartingLocation;
    private int TransitionLocation;

    public float height = 1;

    public float shakeAmount;

    private float startLocationx;
    private float startLocationy;

    
    public GameObject textCountDown;
    private TMP_Text text;

    private bool _stop = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = progress.GetComponent<SpriteRenderer>();

        StartingLocation = (int)progress.transform.localPosition .x;
        TransitionLocation = (int)destination.transform.localPosition .x;

        startLocationx = destination.transform.localPosition.x;
        startLocationy = destination.transform.localPosition.y;

        text = textCountDown.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!_stop)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                progress.transform.localPosition  = transform.right * (StartingLocation + ((StartingLocation - TransitionLocation) * ((timeRemaining)/(totalTime))) - (StartingLocation - TransitionLocation)) + (transform.up * height);
            }
            
            if((int)Mathf.Ceil(timeRemaining) <= 3)
            {
                float x = startLocationx + Random.Range(-shakeAmount, shakeAmount);
                float y = startLocationy + Random.Range(-shakeAmount, shakeAmount);
                float z = destination.transform.localPosition.z;
            
                // Then assign a new vector3
                destination.transform.localPosition = new Vector3 (x, y, z);
                text.text = ((int)Mathf.Ceil(timeRemaining)).ToString();
                // switch(Math.Floor(timeRemaining))
                // {
                //     case 3:
                //         text.text = ((int)3).ToString();
                //         // progress.transform.localPosition = (transform.right * (TransitionLocation - 1 + timeRemaining - 3)) + (transform.up * height);
                //         break;
                //     case 2:
                //         text.text = ((int)2).ToString();
                //         // progress.transform.localPosition = (transform.right * (TransitionLocation - 2 + timeRemaining - 2)) + (transform.up * height);
                //         break;
                //     case 1:
                //         text.text = ((int)1).ToString();
                //         break;
                //     case 0:
                //         text.text = ((int)0).ToString();
                //         break;
                //     default:
                //         // Debug.Log(((StartingLocation - TransitionLocation) * ((timeRemaining-4)/(totalTime-4))));
                //         break;

                // }
            }
        }


    }

    public void stop()
    {
        _stop = true;
    }

    public void start()
    {
        _stop = false;
    }
}
