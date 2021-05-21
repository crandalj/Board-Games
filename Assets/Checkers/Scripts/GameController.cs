using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gamePiece;
    public GameObject tilePiece;
    public GameObject[,] tiles;
    public List<GameObject> gamePieces;

    public Material playerOneMaterial;
    public Material playerTwoMaterial;
    public Material boardOneMaterial;
    public Material boardTwoMaterial;

    private bool isPlaying;
    private bool isPlayerTurn;
    
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
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn && isPlaying)
        {

        }
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
        tiles = new GameObject[8, 8];

        // create game board and pieces
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                // Instantiate tile
                SpawnTilePiece(i, j);
                Tile tile = tiles[i, j].GetComponent<Tile>();

                // assign kings row
                if (i == 0 || i == 7)
                {
                    tile.isKingTile = true;
                }

                // assign game piece
                if(i != 3 && i != 4)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        SpawnGamePiece(i, j);
                    }
                    else
                    {
                        tile.gamePiece = null;
                    }
                }
            }
        }
    }

    private void NewTurn()
    {
        
    }

    private void SpawnGamePiece(int row, int col)
    {
        Vector3 pos = new Vector3(row - 3.5f, 0.25f, col - 3.5f);
        GameObject piece = Instantiate(gamePiece, pos, Quaternion.identity);

        // assign piece color
        MeshRenderer pieceMat = piece.GetComponent<MeshRenderer>();
        if (row == 0 || row == 1 || row == 2)
        {
            pieceMat.material = playerOneMaterial;
        }
        else
        {
            pieceMat.material = playerTwoMaterial;
        }

        gamePieces.Add(piece);
    }

    private void SpawnTilePiece(int row, int col)
    {
        Vector3 pos = new Vector3(row -3.5f, -1f, col -3.5f);
        GameObject piece = Instantiate(tilePiece, pos + Vector3.up, Quaternion.identity);

        // assign tile color
        MeshRenderer pieceMat = piece.GetComponent<MeshRenderer>();
        if ((row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0))
        {
            pieceMat.material = boardOneMaterial;
        }
        else
        {
            pieceMat.material = boardTwoMaterial;
        }

        // add tile to board
        tiles[row, col] = piece;
    }
}
