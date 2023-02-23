using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleShitBoard : GameWinCondition
{
    public Grid tilemap = null;

    public GameObject highlightedSquare;

    private Vector3Int highlightPos;
    
    public GameObject hitTemplate;
    public GameObject missTemplate;
    public GameObject[,] guesses;
    public int[,] ship;

    private bool rand = false;

    private int [] shipFoundCount;
    
    public GameObject[] shipObjects;

    // Start is called before the first frame update
    void Start()
    {
        guesses = new GameObject[8,8];
        shipFoundCount = new int[5];
        for(int i = 0; i < shipObjects.Length; i++)
        {
            if(shipObjects[i] != null)
                shipObjects[i].SetActive(false);
        }

        highlightedSquare.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!rand)
        {
            randomizeGame();
            rand = true;
        }
        if(!stop)
        {
            Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(tilemapPos.x >= 0 && tilemapPos.y >= 0 && tilemapPos.x < 8 && tilemapPos.y < 8)
            {
                highlightedSquare.transform.localPosition = ((Vector3)tilemapPos * .5f) + new Vector3(-1.5f,-1.5f,0f);
                highlightedSquare.SetActive(true);
                if(Input.GetMouseButtonDown(0))
                {
                    if(guesses[tilemapPos.x,tilemapPos.y] == null)
                    {
                        if(ship[tilemapPos.x,tilemapPos.y] != 0)
                        {
                            shipFoundCount[ship[tilemapPos.x,tilemapPos.y]]++;
                            if(shipFoundCount[ship[tilemapPos.x,tilemapPos.y]] == ship[tilemapPos.x,tilemapPos.y])
                            {
                                shipObjects[ship[tilemapPos.x,tilemapPos.y]].SetActive(true);
                                bool allFound = true;
                                for(int i = 4; i >= 2; i--)
                                {
                                    if(shipFoundCount[i] != i)
                                    {
                                        allFound = false;
                                    }
                                }
                                if(allFound)
                                {
                                    win = true;
                                }
                            }
                            guesses[tilemapPos.x,tilemapPos.y] = Instantiate(hitTemplate,transform);
                        }
                        else
                        {
                            guesses[tilemapPos.x,tilemapPos.y] = Instantiate(missTemplate,transform);
                        }
                        guesses[tilemapPos.x,tilemapPos.y].transform.localPosition = ((Vector3)tilemapPos * .5f) + new Vector3(-1.5f,-1.5f,0f);
                        Debug.Log(tilemapPos);
                    }
                }
            }else
            {
                highlightedSquare.SetActive(false);
            }
        }
        if(win)
        {
            // winningPiece.transform.localPosition = Vector3.SmoothDamp(winningPiece.transform.localPosition, ((Vector3)winningLocation * .5f) + new Vector3(-1.5f,-1.5f,0f), ref velocity, smoothTime);
        }
    }

    void randomizeGame()
    {
        ship = new int[8,8];
        int rand = Random.Range(0,1);
        switch(rand)
        {
            case 0:
                for(int shipSize = 4; shipSize > 1; shipSize--)
                {                    
                    int lower = shipSize-1;
                    int upper = 8-shipSize;
                    for(int i = 0; i < 10; i++)
                    {
                        Debug.Log(shipSize);
                        int x,y;
                        if(Random.Range(0,2) == 0)
                        {                            
                            x = Random.Range(lower,upper);
                            y = Random.Range(0,8);
                        }else
                        {                            
                            y = Random.Range(lower,upper);
                            x = Random.Range(0,8);
                        }
                        int directionRand = Random.Range(0,4);
                        Vector2 direction = new Vector2(0,0);
                        switch(directionRand)
                        {
                            case 0:
                                direction = new Vector2(-1,0);
                            break;
                            case 1:
                                direction = new Vector2(0,-1);
                            break;
                            case 2:
                                direction = new Vector2(1,0);
                            break;
                            case 3:
                                direction = new Vector2(0,1);
                            break;
                        }
                        bool noneFound = true;
                        for(int checkPos = 0; checkPos < shipSize; checkPos++)
                        {
                            Vector2 checkDir = new Vector2(x,y) + (direction * checkPos);
                            Debug.Log(checkDir);
                            if(checkDir.x < 0 || checkDir.x > 7 || checkDir.y < 0 || checkDir.y > 7 )
                            {
                                Debug.Log("out");
                                noneFound = false;
                                break;
                            }
                            if(ship[(int)checkDir.x,(int)checkDir.y] != 0)
                            {
                                Debug.Log("found");
                                noneFound = false;
                                break;
                            }
                        }
                        if(noneFound)
                        {
                            for(int checkPos = 0; checkPos < shipSize; checkPos++)
                            {
                                Vector2 checkDir = new Vector2(x,y) + (direction * checkPos);
                                Debug.Log(checkDir);
                                ship[(int)checkDir.x,(int)checkDir.y] = shipSize;
                            }
                            break;
                        }
                        //check positions
                    }
                }
            break;
        }
    }
}
