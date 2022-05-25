using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class CreateBoard : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Board board = (Board)target;
        if (GUILayout.Button("Create Board")) {
            Debug.Log("we got custom button!");
            GameObject tile = board.transform.GetChild(0).gameObject;
            GameObject duplicate = Instantiate(tile);
            duplicate.transform.position += new Vector3(1, 0, 0);
        }
    }
}
