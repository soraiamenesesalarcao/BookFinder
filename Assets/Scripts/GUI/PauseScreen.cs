using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {

    public GUIStyle TitleStyle;

    private Rect backgroundRect;
    private Rect menuRect;
    private Rect innerRect;
    private Rect howRect;

    private Texture2D backgroundTex;
    private Texture2D menuTex;

    private Color backgroundColor;

    private Definitions.GameMode mode;

    private bool howToPlay = false;

    void Awake() {

    }

    void Start() {

        backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        backgroundRect = new Rect(0, 0, Screen.width, Screen.height);
        menuRect = new Rect(Screen.width / 2 - 150, Screen.height / 3, 300, 220);
        innerRect = new Rect(menuRect.x + 20, menuRect.y, menuRect.width - 40, menuRect.height - 50);
        howRect = new Rect(Screen.width / 2 - 300, 100, 600, 400);

        backgroundTex = GUIUtils.MakeTexture((int)backgroundRect.width, (int)backgroundRect.height, backgroundColor);
    }

    void Update() {
        mode = GameState.Instance.CurrentMode;

    }

    void OnGUI() {
        if (mode == Definitions.GameMode.PAUSE) {

            GUI.DrawTexture(backgroundRect, backgroundTex);

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
                GUILayout.Label("Paused game", TitleStyle);
                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Resume game", GUILayout.Width(150))) {
                    if (Definitions.Debug) Debug.Log("Voltar");
                    GameState.Instance.SwitchPause();
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
                if (GUILayout.Button("New game", GUILayout.Width(150))) {
                    if (Definitions.Debug) Debug.Log("Começar de novo");
                    GameState.Instance.FreezeGame(false);
                    GameState.Instance.VisibleInventory = false;
                    GameState.Instance.ChangeLevel(Definitions.Levels.START);
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

   
}
