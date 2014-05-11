using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

public class GameState : MonoBehaviour {

    public static GameState Instance;

    public static HUD HudGUI;
    public static PauseScreen PauseGUI;

    public static Definitions.Levels CurrentLevel;
    public static Definitions.GameMode CurrentMode;

    public static SortedDictionary<string, PlayerScore> Players;
    public static PlayerScore CurrentPlayer;
    public static bool HasWon = true;

    public float TimeGame { get; set; }      

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        TimeGame = 0;
    }

	void Start () {        
        if (CurrentLevel == Definitions.Levels.START) {
            HudGUI = new HUD();
            PauseGUI = new PauseScreen();
            Players = new SortedDictionary<string, PlayerScore>();
            ReadPlayersFromFile(Definitions.PLAYERS_FILE);
        }                
	}
    	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {

        //if (CurrentLevel == Definitions.Levels.GAME) {
        if (CurrentLevel == Definitions.Levels.END) {
            HudGUI.Draw();

            if (CurrentMode == Definitions.GameMode.PAUSE) {
                PauseGUI.Draw();
            }
        }
    }

    // Game End
    public void EndGame() {
        // apurar condicoes de vitoria
        // parar o timer

        // to be done here
        //AddCurrentPlayerToBestScores();
        //WritePlayersToFile(Definitions.PLAYERS_FILE);

        // mudar de cena
        ChangeLevel(Definitions.Levels.END);
    }

    // Game duration
    public float TimeRemaining() {
        float time = Definitions.DURATION - TimeGame;
        if (time > 0) 
            return time;
        else return 0;
    }

    // Level change
    public void ChangeLevel(Definitions.Levels level) {
        CurrentLevel = level;
        Application.LoadLevel((int)level);
    }

    // Current Player
    public void SetCurrentPlayer(string playerName) {
        CurrentPlayer = new PlayerScore(playerName, 0);

        // cheating - MUAHAHAHAHAHAH
        CurrentPlayer.Score += 3;
    }

    public void AddCurrentPlayerToBestScores() {
        PlayerScore ps;

        if (Players.TryGetValue(CurrentPlayer.Name, out ps)) {
            if (CurrentPlayer.Score > ps.Score) {
                ps.Score = CurrentPlayer.Score;
            }
        }
        else Players.Add(CurrentPlayer.Name, CurrentPlayer);
    }

    // Players info
    void ReadPlayersFromFile(string filename) {
        string name;
        int score;
        PlayerScore ps;

        XmlDocument document = new XmlDocument();
        document.Load(filename);
        XmlElement docElement = document.DocumentElement;
        XmlNodeList xmlNodes = docElement.SelectNodes("/players/player");
        
        foreach (XmlNode node in xmlNodes) {
            name = node["name"].InnerText;            
            
            if(Int32.TryParse(node["score"].InnerText, out score)) {

                if (Definitions.Debug) Debug.Log("Name: " + name);
                if (Definitions.Debug) Debug.Log("Score: " + score);

                ps = new PlayerScore(name, score);
                Players.Add(name, ps);
            }            
        }
    }
      

    public void WritePlayersToFile(string filename) {
        string name;
        PlayerScore ps;
        bool HasCurrentPlayer = false;

        XmlDocument document = new XmlDocument();
        document.Load(filename);
        XmlElement docElement = document.DocumentElement;
        XmlNodeList xmlNodes = docElement.SelectNodes("/players/player");

        foreach (XmlNode node in xmlNodes) {
            name = node["name"].InnerText;
            
            if (Players.TryGetValue(name, out ps)) {
                if (Definitions.Debug) Debug.Log("New Score: " + ps.Score);
                node["score"].InnerText = "" + ps.Score;
            }

            if (name.Equals(CurrentPlayer.Name)) {
                HasCurrentPlayer = true;
            }
        }


       if(!HasCurrentPlayer) {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "player", null);

            XmlNode nodeName = document.CreateElement("name");

            nodeName.InnerText = CurrentPlayer.Name;

            XmlNode nodeScore = document.CreateElement("score");
            nodeScore.InnerText = "" + CurrentPlayer.Score;

            newNode.AppendChild(nodeName);
            newNode.AppendChild(nodeScore);

            document["players"].AppendChild(newNode);
        }
        document.Save(Definitions.PLAYERS_FILE);
    }

}
