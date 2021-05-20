using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gamePiece;
    public Tile[,] tiles;

    private bool isPlaying;
    private bool isPlayerTurn;
    private List<GameObject> gamePieces;


    // Singleton stuff
    private static GameController instance;
    public GameController(){}
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePieces = new List<GameObject>();
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NewGame()
    {
        isPlaying = false;

        // check gamePieces, if present then destroy them
        if(gamePieces.Count > 0)
        {
            foreach(GameObject item in gamePieces)
            {
                Destroy(item);
            }
        }
        // initialize list
        gamePieces = new List<GameObject>();

        // populate player tiles
        // row 0, 1, 2
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                // assign kings row
                if(i == 0)
                {
                    tiles[i,j].isKingTile = true;
                }
                // assign game piece
                if((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                {
                    SpawnGamePiece(i, j);
                }
                else
                {
                    tiles[i,j].gamePiece = null;
                }
            }
        }
        // refresh rows 3, 4
        for(int i = 3; i < 5; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                tiles[i,j].gamePiece = null;
            }
        }
        // populate computer tiles
        // row 5, 6, 7
        for (int i = 5; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // assign kings row
                if (i == 7)
                {
                    tiles[i,j].isKingTile = true;
                }
                // assign game piece
                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                {
                    SpawnGamePiece(i, j);
                }
                else
                {
                    tiles[i,j].gamePiece = null;
                }
            }
        }
    }

    private void NewTurn()
    {

    }

    private void SpawnGamePiece(int row, int col)
    {
        Tile tile = tiles[row,col];
        GameObject piece = Instantiate(gamePiece, tile.gameObject.transform.position + Vector3.up, Quaternion.identity);
    }
}
