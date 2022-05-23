using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        GameObject tile = this.transform.GetChild(0).gameObject;
        GameObject duplicate = Instantiate(tile);
        duplicate.transform.position += new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
