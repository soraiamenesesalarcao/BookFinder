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

            // temporario
            Rect R = new Rect(0, 0, Screen.width, Screen.height);
            GUI.Box(R, "Pausa: " + GameState.HasWon);
            
        }
    }

   
}
