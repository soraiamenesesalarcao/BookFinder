﻿using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

    public GUIStyle TitleStyle;
    public GUIStyle LabelStyle;
    public GUIStyle ListStyle;

    public Texture2D Background;

    private Rect backgroundRect;
    private Rect menuRect;
    private Rect innerRect;

    private string message;


	void Start () {

        backgroundRect = new Rect(0, 0, Screen.width, Screen.height);
        menuRect = new Rect(Screen.width / 2 - 150, Screen.height / 4, 300, 220 + 10 * Definitions.MAX_BEST_PLAYERS);
        innerRect = new Rect(menuRect.x + 20, menuRect.y, menuRect.width - 40, menuRect.height - 50);

        GameState.Instance.AddCurrentPlayerToBestScores();
        GameState.Instance.WritePlayers();
	}
	
	void Update () {
	
	}

    void OnGUI() {        

        GUI.DrawTexture(backgroundRect, Background);
        GUI.Box(menuRect, "");
        GUILayout.BeginArea(innerRect);
        GUILayout.Space(20);
        GUILayout.Label("GAME OVER!", TitleStyle);
        GUILayout.Space(20);

        message = (GameState.HasWon) ?  "You won!" : "You lost!";
        GUILayout.Label(message, LabelStyle);

        GUILayout.Space(15);
        GUILayout.Label("Score: " + GameState.CurrentPlayer.Score, LabelStyle);

        GUILayout.Space(15);
        GUILayout.Label("Top " + Definitions.MAX_BEST_PLAYERS + " Scores:", LabelStyle);
        
        GUILayout.Space(15);
        GUILayout.Label("#. Player[Best Score]", LabelStyle);
        GUILayout.Space(10);
        PrintPlayers();

        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play again!")) {            
            if (Definitions.Debug) Debug.Log("Here we go again...");
            GameState.Instance.ChangeLevel(Definitions.Levels.START);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("Quit")) {
            if (Definitions.Debug) Debug.Log("Não desISTa!");
            Application.Quit();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }


    public void PrintPlayers() {
        int i = 0;

        // TO DO
        //  - Ordenar por pontuacao
        //  - Apresentar apenas os top melhores
        foreach (PlayerScore p in GameState.Players.Values) {
            i++;
            GUILayout.Label(i + ". " + p.ToString(), ListStyle);            
        }
    }

}
