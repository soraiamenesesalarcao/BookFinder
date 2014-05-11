using UnityEngine;

public class StartScreen : MonoBehaviour {

    public GUIStyle TitleStyle;
    public GUIStyle LabelStyle;

    public Texture2D Background;

    private Rect backgroundRect;
    private Rect menuRect;
    private Rect innerRect;
    private string playerName;

 
    void Start() {

        backgroundRect = new Rect(0, 0, Screen.width, Screen.height);
        menuRect = new Rect(Screen.width / 2 - 150, Screen.height / 3, 300, 170);
        innerRect = new Rect(menuRect.x + 20, menuRect.y, menuRect.width - 40, menuRect.height - 50);
        playerName = "";
    }


    void Update() {

    }

    void OnGUI() {
        GUI.DrawTexture(backgroundRect, Background);
        GUI.Box(menuRect, "");
        GUILayout.BeginArea(innerRect);
            GUILayout.Space(20);    
            GUILayout.Label("The Game!", TitleStyle);
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Insert your name:", LabelStyle, GUILayout.Width(120), GUILayout.Height(22));
                GUILayout.FlexibleSpace();
                playerName = GUILayout.TextField(playerName, 125, GUILayout.Width(125));
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
                if (GUILayout.Button("Start game")) {
                    if (playerName != "") {
                        if(Definitions.Debug) Debug.Log("Let The Game begin...");
                        GameState.Instance.SetCurrentPlayer(playerName);                       
                        //GameState.Instance.ChangeLevel(Definitions.Levels.GAME);
                        GameState.Instance.ChangeLevel(Definitions.Levels.END);
                    }
                    else {
                        if (Definitions.Debug) Debug.Log("InsISTo que insira o seu nome!");
                    }                    
                }
                GUILayout.Space(20);
                if (GUILayout.Button("Quit")) {
                    if (Definitions.Debug) Debug.Log("Não desISTa!");
                    Application.Quit();
                }
            GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
