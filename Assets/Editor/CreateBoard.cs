using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class CreateBoard : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Board board = (Board)target;
        if (GUILayout.Button("Create Board")) {
            //Debug.Log("test");
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    var tile = board.transform.GetChild(0).gameObject;
                    var duplicate = Instantiate(tile, board.transform);
                    duplicate.transform.position += new Vector3(i + 1, 0, j);
                }
            }
        }
    }
}
