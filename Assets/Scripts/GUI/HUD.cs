using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public static bool IsInventoryVisible = false;

    public GUIStyle TexStyle;
    public GUIStyle LabelStyle;
    public GUIStyle ValueStyle;
    public GUIStyle ItemCountStyle;

    public Texture2D LifeTex;
    public Texture2D OrangeTex;
    public Texture2D YellowTex;
    public Texture2D BlueTex;
    public Texture2D BordeauxTex;
    private Texture2D infoBackgroundTex;
    private Texture2D inventoryBackgroundTex;
    private Texture2D inventoryHiddenBackgroundTex;

    private Rect infoBackRect;
    private Rect inventoryBackRect;
    private Rect inventoryHiddenBackRect;

    private Rect infoRect;
    private Rect inventoryRect;
    private Rect inventoryHiddenRect;

    private int rectOffset = 20;

    private Color backgroundColor;

    void Awake() {      
        
    }

    void Start() {
        backgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);

        infoBackRect = new Rect(rectOffset, rectOffset, 175, 125);
        infoRect = new Rect(infoBackRect.x + rectOffset, infoBackRect.y + rectOffset, 
                            infoBackRect.width - 2 * rectOffset, infoBackRect.height - 2 * rectOffset);

        inventoryBackRect = new Rect(rectOffset, Screen.height - 400 - rectOffset, 175, 400);
        inventoryRect = new Rect(inventoryBackRect.x + rectOffset, inventoryBackRect.y + rectOffset,
                                       inventoryBackRect.width - rectOffset, inventoryBackRect.height - rectOffset); 
               
        inventoryHiddenBackRect = new Rect(rectOffset, Screen.height - 70 - rectOffset, 175, 70);
        inventoryHiddenRect = new Rect(inventoryHiddenBackRect.x + rectOffset, inventoryHiddenBackRect.y + rectOffset,
                                       inventoryHiddenBackRect.width - rectOffset, inventoryHiddenBackRect.height - rectOffset);
              
        infoBackgroundTex = GUIUtils.MakeTexture((int)infoBackRect.width, (int)infoBackRect.height, backgroundColor);
        inventoryBackgroundTex = GUIUtils.MakeTexture((int)inventoryBackRect.width, (int)inventoryBackRect.height, backgroundColor);
        inventoryHiddenBackgroundTex = GUIUtils.MakeTexture((int)inventoryHiddenBackRect.width, (int)inventoryHiddenBackRect.height, backgroundColor);        
    }

    void Update() {
        IsInventoryVisible = GameState.Instance.VisibleInventory;
    }

    void OnGUI() {
        DrawInfo();
        DrawInventory(); 
    }

    void DrawInfo() {
        GUI.DrawTexture(infoBackRect, infoBackgroundTex);
        GUILayout.BeginArea(infoRect);

            // Life
            GUILayout.BeginHorizontal(GUILayout.Width(100));
                for (int i = 0; i < GameState.CurrentPlayer.NumberOfLives; i++) {
                    GUILayout.Label(LifeTex);
                    GUILayout.Space(5);
                }
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // Score
            GUILayout.BeginHorizontal();
            GUILayout.Label("Score: ", LabelStyle, GUILayout.Width(75));
                GUILayout.Space(10);
                GUILayout.Label("" + GameState.CurrentPlayer.Score, ValueStyle);
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // Time remaining
            GUILayout.BeginHorizontal();
            GUILayout.Label("Time Left: ", LabelStyle, GUILayout.Width(75));
                GUILayout.Space(10);
                GUILayout.Label(GUIUtils.SecondsToHMS(GameState.Instance.TimeRemaining()), ValueStyle);
            GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void DrawInventory() {
        
        if (IsInventoryVisible) {
            GUI.DrawTexture(inventoryBackRect, inventoryBackgroundTex);
            GUILayout.BeginArea(inventoryRect);

            // Books
            GUILayout.BeginHorizontal();
            GUILayout.Label(BlueTex, GUILayout.Width(70));
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("" + GameState.CurrentPlayer.Items.book_blue, ItemCountStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label(BordeauxTex, GUILayout.Width(70));
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("" + GameState.CurrentPlayer.Items.book_bordeaux, ItemCountStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label(OrangeTex, GUILayout.Width(70));
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("" + GameState.CurrentPlayer.Items.book_orange, ItemCountStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label(YellowTex, GUILayout.Width(70));
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("" + GameState.CurrentPlayer.Items.book_yellow, ItemCountStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("I : Hide Inventory");
            GUILayout.Space(5);

            GUILayout.EndArea();
        }
        else {
            GUI.DrawTexture(inventoryHiddenBackRect, inventoryHiddenBackgroundTex);
            GUILayout.BeginArea(inventoryHiddenRect);
                GUILayout.Space(5);
                GUILayout.Label("'I' : Show Inventory");
                GUILayout.Space(5);
            GUILayout.EndArea();
        }
    }

}
