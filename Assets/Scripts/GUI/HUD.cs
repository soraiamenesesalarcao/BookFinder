using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public static bool IsInventoryVisible = false;
    public CharacterInfo CharacterInstance;

    public GUIStyle TexStyle;
    public GUIStyle LabelStyle;
    public GUIStyle ValueStyle;

    public Texture2D LifeTex;

    private Rect infoRect;

    void Awake() {      
        

    }

    void Start() {
        infoRect = new Rect(20, 20, 250, 200);
       
        //CharacterInstance = GameObject.Find("Molly").GetComponent(typeof(CharacterInfo)) as CharacterInfo;
        if (GameObject.Find("Player") != null)
            CharacterInstance = GameObject.Find("Player").GetComponent(typeof(CharacterInfo)) as CharacterInfo;
    }

    void Update() {       

        //CharacterInstance = GameObject.Find("Molly").GetComponent(typeof(CharacterInfo)) as CharacterInfo;
        if (CharacterInstance != null && GameObject.Find("Player") != null)
            CharacterInstance = GameObject.Find("Player").GetComponent(typeof(CharacterInfo)) as CharacterInfo;
    }

    void OnGUI() {        

        if (CharacterInstance != null) {
            DrawInfo();
            DrawMap();
            if (IsInventoryVisible)
                DrawInventory();
        }
        
    }

    void DrawInfo() {
        GUILayout.BeginArea(infoRect);

            // Life
            GUILayout.BeginHorizontal(GUILayout.Width(100));
                for (int i = 0; i < CharacterInstance.NumberOfLives; i++) {
                    GUILayout.Box(LifeTex, TexStyle);
                    GUILayout.Space(5);
                }
            GUILayout.EndHorizontal();
            
            // Score
            GUILayout.BeginHorizontal();
            GUILayout.Label("Score: ", GUILayout.Width(75));
                GUILayout.Space(10);
                GUILayout.Label("" + CharacterInstance.Score);
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

    void DrawMap() {
    }
}
