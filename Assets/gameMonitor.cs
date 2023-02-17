using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMonitor : MonoBehaviour
{
    public CountDownBar countDown;

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
        }
    }
}
