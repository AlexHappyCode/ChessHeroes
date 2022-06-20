using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Board : MonoBehaviour {
    [SerializeField]
    public GameObject tile;

    //private GameObject[,] tiles = new GameObject[TILE_COUNT_Z, TILE_COUNT_X];
    List<List<GameObject>> tiles = new List<List<GameObject>>();  //Create 2d List

    private Camera currentCamera;
    private Vector2Int currentHover;

    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Z = 8;

    public void CreateBoard() {
        for (int z = 0; z < TILE_COUNT_Z; z++) {
            tiles.Add(new List<GameObject>());
            for (int x = 0; x < TILE_COUNT_X; x++) {
                var newTile = Instantiate(tile, transform);
                newTile.transform.position += new Vector3(x, 0, z);
                newTile.name = "tile " + newTile.transform.position.z + " " + newTile.transform.position.x;
                tiles[z].Add(newTile);
            }
        }
    }

    public void Update() {
        if (!currentCamera) {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        // this is cool as shit
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile"))) {
            //Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);
            var hitPosition = new Vector2Int(0, 0);
            if (currentHover == -Vector2Int.one) {
                currentHover = hitPosition;
                //tiles[hitPosition.y][hitPosition.x].layer = LayerMask.NameToLayer("Hover");
            }
            if (currentHover != -hitPosition) {
                //tiles[currentHover.y][currentHover.x].layer = LayerMask.NameToLayer("Tile");
                currentHover = hitPosition; 
                //tiles[hitPosition.y][hitPosition.x].layer = LayerMask.NameToLayer("Hover");
            }
        }
        else {
            if (currentHover != -Vector2Int.one) { 
                //tiles[currentHover.y][currentHover.x].layer = LayerMask.NameToLayer("Tile");
            }
        }
    }
    private Vector2Int LookupTileIndex(GameObject hitInfo) {
        for (int z = 0; z < TILE_COUNT_Z; z++) {
            for (int x = 0; x < TILE_COUNT_X; x++) {
                if (tiles[z][x] == hitInfo)
                    return new Vector2Int(x, z);
            }
        }
        return -Vector2Int.one; // Invalid
    }
}
   