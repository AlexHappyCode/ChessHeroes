using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class CreateBoard : Editor {
    

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var board = (Board)target;
        
        if (GUILayout.Button("Create Board")) {
            board.CreateBoard();
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
