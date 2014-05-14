using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {

    public GUIStyle TitleStyle;

    private int rectOffset = 20;

    private Rect backgroundRect;
    private Rect menuRect;
    private Rect innerRect;

    private Texture2D backgroundTex;
    private Texture2D menuTex;

    private Color backgroundColor;
    private Color menuColor;

    private Definitions.GameMode mode;

    void Awake() {

    }

    void Start() {

        backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        menuColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);

        backgroundRect = new Rect(0, 0, Screen.width, Screen.height);
        menuRect = new Rect(Screen.width / 2 - 150, Screen.height / 3, 300, 200);
        innerRect = new Rect(menuRect.x + 20, menuRect.y, menuRect.width - 40, menuRect.height - 50);

        backgroundTex = GUIUtils.MakeTexture((int)backgroundRect.width, (int)backgroundRect.height, backgroundColor);
    }

    void Update() {
        mode = GameState.Instance.CurrentMode;

    }

    void OnGUI() {
        if (mode == Definitions.GameMode.PAUSE) {

            GUI.DrawTexture(backgroundRect, backgroundTex);
            GUI.Box(menuRect, "");
            GUILayout.BeginArea(innerRect);

            GUILayout.Space(20);
            GUILayout.Label("Paused game", TitleStyle);
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Resume game", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Back on track...");
                GameState.Instance.SwitchPause();               
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("How to Play", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Vamos lá aprender!");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Quit", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Não desISTa!");
                Application.Quit();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
            
        }
    }

   
}
