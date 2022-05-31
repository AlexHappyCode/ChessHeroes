using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour {

    public Sprite[] diceFaces;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    void RollTheDice() {
        int diceResult = Random.Range(0, 7);
        Debug.Log("diceResult: " + diceResult);
        this.transform.GetChild(0).GetComponent<Image>().sprite = 
            diceFaces[diceResult];
    }
}
