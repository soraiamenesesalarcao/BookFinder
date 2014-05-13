using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {

    private Definitions.GameMode mode;

    void Awake() {

    }

    void Start() {
       
    }

    void Update() {
        mode = GameState.Instance.CurrentMode;

    }

    void OnGUI() {
        if (mode == Definitions.GameMode.PAUSE) {

            Debug.Log("pausa");
            // experiencia
            Rect R = new Rect(0, 0, Screen.width, Screen.height);
            GUI.Box(R, "" + GameState.HasWon);
            // end of experiencia 
        }
    }

   
}
