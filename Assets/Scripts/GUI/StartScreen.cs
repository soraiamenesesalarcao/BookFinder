using UnityEngine;

public class StartScreen : MonoBehaviour {

    public GUIStyle TitleStyle;
    public GUIStyle LabelStyle;

    public Texture2D Background;

    private Rect backgroundRect;
    private Rect menuRect;
    private Rect innerRect;
    private Rect howRect;

    private string playerName;

    private bool howToPlay = false;

 
    void Start() {
        backgroundRect = new Rect(0, 0, Screen.width, Screen.height);
        menuRect = new Rect(Screen.width / 2 - 150, Screen.height / 3, 300, 240);
        howRect = new Rect(Screen.width / 2 - 300, 100, 600, 400);
        innerRect = new Rect(menuRect.x + 20, menuRect.y, menuRect.width - 40, menuRect.height - 50);
        playerName = "";
    }


    void Update() {

    }

    void OnGUI() {
        GUI.DrawTexture(backgroundRect, Background);      

        if (howToPlay) {
            GUI.Box(howRect, "");
            GUILayout.BeginArea(howRect);            
            GUILayout.Space(20);
            GUILayout.Label("BookFinder: How to play", TitleStyle);
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Molly and Wukong love reading. Unfortunately," + 
                            "Mukong was kidnapped by an evil monster called Groucho, " +
                            "who also trapped Wukong's books in a maze. " + 
                            "Now Molly's mission is to find his books as fast as she can " + 
                            "and make it to the end of the maze in order to saver her friend.", GUILayout.Width(550));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("ESC: Pause / Resume game", GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("I: Show / hide inventory", GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Arrows / W / A / S / D: Move Molly ", GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("(forward / left / backwards / right)", GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Mouse motion: Rotate Molly ", GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Back", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Sair da ajuda");
                howToPlay = false;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        else {
            GUI.Box(menuRect, "");
            GUILayout.BeginArea(innerRect);
            GUILayout.Space(20);
            GUILayout.Label("BookFinder!", TitleStyle);
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Insert your name:", LabelStyle, GUILayout.Width(120), GUILayout.Height(22));
            GUILayout.FlexibleSpace();
            playerName = GUILayout.TextField(playerName, 125, GUILayout.Width(125));
            GUILayout.EndHorizontal();
            GUILayout.Space(30);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Start game", GUILayout.Width(150))) {
                if (playerName != "") {
                    if (Definitions.Debug) Debug.Log("Jogar");
                    GameState.Instance.SetCurrentPlayer(playerName);
                    GameState.Instance.ChangeLevel(Definitions.Levels.GAME);
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("How to play", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Ajuda");
                howToPlay = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Quit", GUILayout.Width(150))) {
                if (Definitions.Debug) Debug.Log("Sair");
                Application.Quit();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
