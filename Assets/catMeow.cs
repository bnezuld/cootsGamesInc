using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMeow : MonoBehaviour
{
    private SpriteRenderer spriteRend;

    public Sprite[] cats;

    // Start is called before the first frame update
    void Start()
    {
        float newScale = Random.Range(2,4)/10.0f;
        transform.localScale = new Vector3(newScale, newScale, 0);

        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = cats[Random.Range(0,cats.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
