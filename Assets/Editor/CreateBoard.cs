using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class CreateBoard : Editor {
    

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var board = (Board)target;
        
        if (GUILayout.Button("Create Board")) {
            //Debug.Log("test");
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    var newTile = Instantiate(board.tile, board.transform);
                    newTile.transform.position += new Vector3(j, 0, i);
                    newTile.name = "tile " + newTile.transform.position.z + " " + newTile.transform.position.x;
                }
            }
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
