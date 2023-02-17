using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class intro : MonoBehaviour
{
    
    public TMP_Text livesText;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "lives: " +  gameMonitor.lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
