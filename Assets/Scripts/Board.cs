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

    public void update() {
        if (!currentCamera) {
            currentCamera = Camera.current;
            return;
        }

        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile"))) {
            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);
        }
    }

    private Vector2Int LookupTileIndex(GameObject hitInfo) {
        for (int x = 0; x < TILE_COUNT_X; x++) {
            for (int z = 0; z < TILE_COUNT_Z; z++) {
                if (tiles[x][z] == hitInfo) {
                    return new Vector2Int(x, z);
                }
            }
        }
        return -Vector2Int.one; // Invalid
    }
}
   