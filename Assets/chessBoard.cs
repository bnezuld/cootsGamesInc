using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoard : GameWinCondition
{
    public Grid tilemap = null;

    public GameObject highlightedSquare;

    private Vector3Int highlightPos;

    public GameObject winningPiece;
    public Sprite winningPieceSprite;
    public Vector3Int StartLocation;
    public Vector3Int winningLocation;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private Vector3Int chooseStartLocation;
    private Vector3Int chooseWinningLocation;

    
    public GameObject wrongMove;

    public Sprite[] chessBoards;    
    public Sprite[] chessPieces;

    private enum chessSpriteMapping
    {
        pawn,
        rook,
        knight,
        bishop,
        queen,
        king
    }

    // Start is called before the first frame update
    void Start()
    {
        randomizeGame();

        highlightedSquare.SetActive(false);
        wrongMove.SetActive(false);
        
        SpriteRenderer spriteRender = winningPiece.GetComponent<SpriteRenderer>();
        spriteRender.sprite = winningPieceSprite;
        winningPiece.transform.localPosition = ((Vector3)StartLocation * .5f) + new Vector3(-1.5f,-1.5f,0f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!stop)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Debug.Log(tilemapPos);
                if(tilemapPos.x >= 0 && tilemapPos.y >= 0 && tilemapPos.x < 8 && tilemapPos.y < 8)
                {
                    highlightedSquare.transform.localPosition = ((Vector3)tilemapPos * .5f) + new Vector3(-1.5f,-1.5f,0f);
                    if(highlightedSquare.activeSelf)
                    {
                        highlightedSquare.SetActive(false);
                        chooseWinningLocation = tilemapPos;
                        if(chooseStartLocation == StartLocation && chooseWinningLocation == winningLocation)
                        {
                            winningPiece.transform.localPosition = Vector3.SmoothDamp(winningPiece.transform.localPosition, ((Vector3)winningLocation * .5f) + new Vector3(-1.5f,-1.5f,0f), ref velocity, smoothTime);
                            // winningPiece.transform.localPosition = ;
                            win = true;
                        }else if(chooseStartLocation == chooseWinningLocation)
                        {
                            
                        }else
                        {
                            wrongMove.SetActive(true);
                        }
                    }else
                    {
                        highlightedSquare.SetActive(true);
                        chooseStartLocation = tilemapPos;
                    }
                }
            }
        }
        if(win)
        {
            winningPiece.transform.localPosition = Vector3.SmoothDamp(winningPiece.transform.localPosition, ((Vector3)winningLocation * .5f) + new Vector3(-1.5f,-1.5f,0f), ref velocity, smoothTime);
        }
    }

    void randomizeGame()
    {
        int rand = Random.Range(0,6);
        switch(rand)
        {
            case 0:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.queen];
                StartLocation = new Vector3Int(3, 0, 0);
                winningLocation = new Vector3Int(7, 4, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[0];
            break;
            case 1:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.bishop];
                StartLocation = new Vector3Int(5, 0, 0);
                winningLocation = new Vector3Int(1, 4, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[1];
            break;
            case 2:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.queen];
                StartLocation = new Vector3Int(4, 3, 0);
                winningLocation = new Vector3Int(1, 6, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[2];
            break;
            case 3:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.rook];
                StartLocation = new Vector3Int(4, 0, 0);
                winningLocation = new Vector3Int(4, 7, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[3];
            break;
            case 4:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.knight];
                StartLocation = new Vector3Int(5, 4, 0);
                winningLocation = new Vector3Int(6, 6, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[4];
            break;
            case 5:
                winningPieceSprite = chessPieces[(int)chessSpriteMapping.pawn];
                StartLocation = new Vector3Int(6, 5, 0);
                winningLocation = new Vector3Int(6, 6, 0);
                GetComponent<SpriteRenderer>().sprite = chessBoards[5];
            break;
        }
    }
}
