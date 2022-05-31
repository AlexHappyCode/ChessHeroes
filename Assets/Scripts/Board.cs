using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Board : MonoBehaviour {
    // Start is called before the first frame update
    [Header("Art Stuff")] [SerializeField] private Material tileMaterial;
    
    public GameObject tile;
    
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Z = 8;
    private GameObject[,] tiles;

    private void Awake() {
        generateAllTiles(1, TILE_COUNT_X, TILE_COUNT_Z);
    }

    private void generateAllTiles(float tileSize, int tileCountX, int tileCountZ) {
        tiles = new GameObject[tileCountX, tileCountZ];
        for (int x = 0; x < tileCountX; x++)
        for (int z = 0; z < tileCountZ; z++)
            tiles[x, z] = GenerateSingleTile(tileSize, x, z);
    }

    private GameObject GenerateSingleTile(float tileSize, int x, int z) {
        GameObject tileObject = new GameObject("X: " + x + ", Z: " + z);
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 0, z * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0, (z + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0, z * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0, (z + 1) * tileSize);

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        
        mesh.RecalculateNormals();

        tileObject.AddComponent<BoxCollider>();

        return tileObject;
    }
}
