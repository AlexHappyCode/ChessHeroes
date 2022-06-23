using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Serialization;

public class Board : MonoBehaviour {

    [Header("Prefabs & Materials")] 
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] teamMaterials;
    
    [Header("General")]
    [SerializeField] public GameObject tile1;
    [SerializeField] public GameObject tile2;
    [SerializeField] public Material hoverMaterial;
    [SerializeField] public Material tile1Material;
    [SerializeField] public Material tile2Material;
    [SerializeField] public bool logging;

    readonly List<List<GameObject>> tiles = new();  //Create 2d List
    private ChessPiece[,] chessPieces;
    private Camera currentCamera;
    private Vector2Int currentHover = -Vector2Int.one;
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Z = 8;
    private Material curMaterial = null;

    public void Start() {
        CreateBoard();
        SpawnAllPieces();
        StartingPosition();
    }

    public void Update() {
        TileHover();
    }
    public void CreateBoard() {
         for (int z = 0; z < TILE_COUNT_Z; z++) {
             tiles.Add(new List<GameObject>());
             for (int x = 0; x < TILE_COUNT_X; x++) {
 
                 GameObject newTile;
 
                 if (z % 2 == 0) {
                     newTile = x % 2 == 0 ? Instantiate(tile1, transform) : Instantiate(tile2, transform);
                 }
                 else {
                     newTile = x % 2 == 1 ? Instantiate(tile1, transform) : Instantiate(tile2, transform);
                 }
 
                 newTile.transform.position += new Vector3(x, 0, z);
                 newTile.name = "tile " + newTile.transform.position.z + " " + newTile.transform.position.x;
                 tiles[z].Add(newTile);
             }
         }
    }

    private void SpawnAllPieces() {
        chessPieces = new ChessPiece[TILE_COUNT_Z, TILE_COUNT_X];

        int whiteTeam = 0;
        int blackTeam = 1;

        // white team
        chessPieces[0, 0] = SpawnPiece(ChessPieceType.Rook, whiteTeam);
        chessPieces[0, 1] = SpawnPiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[0, 2] = SpawnPiece(ChessPieceType.Knight, whiteTeam);
        chessPieces[0, 3] = SpawnPiece(ChessPieceType.Queen, whiteTeam);
        chessPieces[0, 4] = SpawnPiece(ChessPieceType.King, whiteTeam);
        chessPieces[0, 5] = SpawnPiece(ChessPieceType.Knight, whiteTeam);
        chessPieces[0, 6] = SpawnPiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[0, 7] = SpawnPiece(ChessPieceType.Rook, whiteTeam);
        
        for (int i = 0; i < TILE_COUNT_X; i++) {
            chessPieces[1, i] = SpawnPiece(ChessPieceType.Pawn, whiteTeam);
        }
        
        // Black Team
        chessPieces[7, 0] = SpawnPiece(ChessPieceType.Rook, blackTeam);
        chessPieces[7, 1] = SpawnPiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[7, 2] = SpawnPiece(ChessPieceType.Knight, blackTeam);
        chessPieces[7, 3] = SpawnPiece(ChessPieceType.Queen, blackTeam);
        chessPieces[7, 4] = SpawnPiece(ChessPieceType.King, blackTeam);
        chessPieces[7, 5] = SpawnPiece(ChessPieceType.Knight, blackTeam);
        chessPieces[7, 6] = SpawnPiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[7, 7] = SpawnPiece(ChessPieceType.Rook, blackTeam);
        
        for (int i = 0; i < TILE_COUNT_X; i++) {
            chessPieces[6, i] = SpawnPiece(ChessPieceType.Pawn, blackTeam);
        }
    }

    private void StartingPosition() {
        for (int z = 0; z < TILE_COUNT_Z; z++) {
            for (int x = 0; x < TILE_COUNT_X; x++) {
                if (chessPieces[z, x] != null) {
                    PositionPiece(z, x, true);
                }
            }
        }
    }
    private void PositionPiece(int z, int x, bool force = false) {
        chessPieces[z, x].currentY = z;
        chessPieces[z, x].currentX = x;
        chessPieces[z, x].transform.position = new Vector3(x, 0.5f, z);
    }

    private ChessPiece SpawnPiece(ChessPieceType type, int team) {
        ChessPiece chessPiece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();
        chessPiece.type = type;
        chessPiece.team = team;
        chessPiece.GetComponent<MeshRenderer>().material = teamMaterials[team];
        return chessPiece;
    }

    private void TileHover() {
        if (!currentCamera) {
            currentCamera = Camera.main;
            return;
        }

        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        // this is cool as shit
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        //var didHit= Physics.Raycast(ray, out RaycastHit info, 100, LayerMask.GetMask("Tile"));
        var didHit = Physics.Raycast(ray, out RaycastHit info, 100);
        if (didHit) {
            if (logging) Debug.Log("raycast hit at: " + info.transform.gameObject);
            
            // get index of tile I hit
            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);
            
            // if we are hovering a tile after not hovering any tiles
            if (currentHover == -Vector2Int.one) {
                currentHover = hitPosition;
                tiles[hitPosition.y][hitPosition.x].layer = LayerMask.NameToLayer("Hover");
                curMaterial = tiles[hitPosition.y][hitPosition.x].GetComponent<MeshRenderer>().material;
                tiles[hitPosition.y][hitPosition.x].GetComponent<MeshRenderer>().material = hoverMaterial;
            }
            
            // if we are already hovering a tile, change the previous one
            if (currentHover != hitPosition) {
                if (logging) {
                    Debug.Log("currentHover != hitPosition");
                    Debug.Log("hitPosition: " + hitPosition);
                    Debug.Log("currentHover position: " + currentHover);
                }

                tiles[currentHover.y][currentHover.x].layer = LayerMask.NameToLayer("Tile");
                tiles[currentHover.y][currentHover.x].GetComponent<MeshRenderer>().material = curMaterial;
                currentHover = hitPosition; 
                tiles[hitPosition.y][hitPosition.x].layer = LayerMask.NameToLayer("Hover");
                curMaterial = tiles[hitPosition.y][hitPosition.x].GetComponent<MeshRenderer>().material;
                tiles[hitPosition.y][hitPosition.x].GetComponent<MeshRenderer>().material = hoverMaterial;
            }
        }
        else {
            if (logging) Debug.Log("NO HIT");
            if (currentHover != -Vector2Int.one) { 
                if (logging) Debug.Log("Did not hit any objects, set current hover to (-1, -1)");
                tiles[currentHover.y][currentHover.x].layer = LayerMask.NameToLayer("Tile");
                tiles[currentHover.y][currentHover.x].GetComponent<MeshRenderer>().material = curMaterial;
                currentHover = -Vector2Int.one;
            }
        }
    }
    
    private Vector2Int LookupTileIndex(GameObject hitInfo) {
        if (logging) Debug.Log("hitInfo: " + hitInfo.transform);
        for (int z = 0; z < TILE_COUNT_Z; z++) {
            for (int x = 0; x < TILE_COUNT_X; x++) {
                if (tiles[z][x] == hitInfo)
                    return new Vector2Int(x, z);
            }
        }
        if (logging) Debug.Log("Shouldn't get here, couldnt find a match for: " + hitInfo);
        return -Vector2Int.one; // Invalid
    }
}
   