using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gamePiece;
    public GameObject tilePiece;
    public GameObject[,] tiles;
    public List<GameObject> gamePieces;
    // add playerPieces and aiPieces to replace gamepieces to keep better track of it

    public Material playerOneMaterial;
    public Material playerTwoMaterial;
    public Material boardOneMaterial;
    public Material boardTwoMaterial;

    private bool isPlaying;
    private bool isPlayerTurn;
    private Tile selectedDestination;  // pieces are associated with their tile so can use the tile to get the piece
    private Tile selectedPiece;  // pieces are associated with their tile so can use the tile to get the piece

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
            // Player turn
        } else if(!isPlayerTurn && isPlaying)
        {
            // AI turn
        }
    }

    private void NewGame()
    {
        isPlaying = false;
        isPlayerTurn = false;

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

        NewTurn();
    }

    private void NewTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    public void CheckGameState()
    {
        // check victory / defeat

        // victory = all AI pieces removed

        // defeat = all player pieces removed
    }

    public void MovePiece(Tile jumpedTile = null)
    {
        // make selectedDestination's tile gameobject that of selectedPieces' and move it
        selectedDestination.gamePiece = selectedPiece.gamePiece;
        //TODO: trigger gamePiece's animation and update position

        // check if jumpedTile is not null, if not remove the piece that was jumped

        // make selectedPieces' tile gameobject null
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
