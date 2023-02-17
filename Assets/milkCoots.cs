using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milkCoots : GameWinCondition
{
    public GameObject cat;
    public Sprite[] catMilk;

    private SpriteRenderer spriteRend;

    public int milkFilled;
    public int milkRequired;

    public Sprite[] milkJugSprites;
    public GameObject MilkJug;
    private SpriteRenderer MilkJugSpriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend =  cat.GetComponent<SpriteRenderer>();
        spriteRend.sprite = catMilk[0];

        MilkJugSpriteRend = MilkJug.GetComponent<SpriteRenderer>();
        MilkJugSpriteRend.sprite = milkJugSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnMouseDown() 
    {
        if(!stop)
        {
            spriteRend.sprite = catMilk[1];
            
            milkFilled++;
            if(milkFilled >= milkRequired)
            {
                win = true;
            }

            int fill = (int)Mathf.Floor(((float)milkFilled/milkRequired*(milkJugSprites.Length-1)));
            if(fill > milkJugSprites.Length - 1)
                fill = milkJugSprites.Length - 1;
            Debug.Log(fill);
            MilkJugSpriteRend.sprite = milkJugSprites[fill];
        }
    }

    void OnMouseUp() 
    {
        spriteRend.sprite = catMilk[0];
    }
}
