using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInOut : MonoBehaviour
{
    private int state = 0;
    private float progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        Color newColor = new Color(1, 1, 1, 0);
        transform.GetComponent<SpriteRenderer>().material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case 0:
            //fade in
            {
                progress += Time.deltaTime*8.0f;
                float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
                Color newColor = new Color(1, 1, 1, Mathf.Sin(progress));//Mathf.Lerp(alpha, 1.0f, Time.deltaTime/1.0f));
                transform.GetComponent<SpriteRenderer>().material.color = newColor;   
                Debug.Log(newColor.a);             
                if(newColor.a >= 0.9f)
                {
                    state = 1;
                }
            }
            break;
            case 1:         
            //fade out   
            {
                progress += Time.deltaTime * 8.0f;
                float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
                Color newColor = new Color(1, 1, 1, Mathf.Sin(progress));//Mathf.Lerp(alpha, 1.0f, Time.deltaTime/1.0f));
                transform.GetComponent<SpriteRenderer>().material.color = newColor;
                Debug.Log(newColor.a);             
                if(newColor.a <= 0.1f)
                {
                    progress = 0;
                    state = 2;
                }
            }
            break;
            case 2:
            {
                Color newColor = new Color(1, 1, 1, 0.0f);//Mathf.Lerp(alpha, 1.0f, Time.deltaTime/1.0f));
                transform.GetComponent<SpriteRenderer>().material.color = newColor;
                state = 0;
                gameObject.SetActive(false);
                break;
            }
        }
    }

    void FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<SpriteRenderer>().material.color = newColor;
            //yield return null;
        }
    }
}
