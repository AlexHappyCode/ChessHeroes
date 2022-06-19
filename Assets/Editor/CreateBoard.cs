using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class CreateBoard : Editor {
    

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var board = (Board)target;
        
        if (GUILayout.Button("Create Board")) {
            board.CreateBoard();
            //Debug.Log("test");
            /*
            for (int x = 0; x < 8; x++) {
                for (int z = 0; z < 8; z++) {
                    var newTile = Instantiate(board.tile, board.transform);
                    newTile.transform.position += new Vector3(z, 0, x);
                    newTile.name = "tile " + newTile.transform.position.z + " " + newTile.transform.position.x;
                    tiles[x, z] = newTile;
                }
            }
            */
        } else if (GUILayout.Button("Empty Board")) {
            //Debug.Log("childCount: " + board.transform.childCount);
            
            GameObject[] allChildren = new GameObject[board.transform.childCount];

            var i = 0;
            foreach (Transform child in board.transform) {
                allChildren[i] = child.gameObject;
                i++;
            }
            
            foreach (GameObject child in allChildren) {
                DestroyImmediate(child);
            }
        }
    }
}
