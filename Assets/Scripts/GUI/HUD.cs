using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public static bool IsInventoryVisible = false;

    public GUIStyle TexStyle;
    public GUIStyle LabelStyle;
    public GUIStyle ValueStyle;

    public Texture2D LifeTex;

    private Rect infoRect;

    void Awake() {      
        
    }

    void Start() {
        infoRect = new Rect(20, 20, 250, 200);
    }

    void Update() {       
    }

    void OnGUI() {
            DrawInfo();
            if (IsInventoryVisible)
                DrawInventory(); 
    }

    void DrawInfo() {
        GUILayout.BeginArea(infoRect);

            // Life
            GUILayout.BeginHorizontal(GUILayout.Width(100));
                for (int i = 0; i < GameState.CurrentPlayer.NumberOfLives; i++) {
                //for (int i = 0; i < GameState.Instance.CurrentPlayer.NumberOfLives; i++) {
                    GUILayout.Box(LifeTex, TexStyle);
                    GUILayout.Space(5);
                }
            GUILayout.EndHorizontal();
            
            // Score
            GUILayout.BeginHorizontal();
            GUILayout.Label("Score: ", GUILayout.Width(75));
                GUILayout.Space(10);
                GUILayout.Label("" + GameState.CurrentPlayer.Score);
                //GUILayout.Label("" + GameState.Instance.CurrentPlayer.Score);
            GUILayout.EndHorizontal();

            // Time remaining
            GUILayout.BeginHorizontal();
            GUILayout.Label("Time left: ", GUILayout.Width(75));
                GUILayout.Space(10);
                GUILayout.Label("" + GameState.TimeGame);
            GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void DrawInventory() {
    }

}
