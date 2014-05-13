using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

public class GameState : MonoBehaviour {

    public static GameState Instance;


    public static Definitions.Levels CurrentLevel;
    public Definitions.GameMode CurrentMode;

    public static SortedDictionary<string, PlayerScore> Players;
    public CharacterInfo CurrentPlayer;
    public static PlayerScore CurrentPlayerScore;
    public static bool HasWon = true;

    public static float TimeGame { get; set; }

    private static float eventTime;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        TimeGame = 0;
    }

    void Start() {
        CurrentMode = Definitions.GameMode.PLAYING;
    }
    	
	// Update is called once per frame
	void Update () {
        if (CurrentMode == Definitions.GameMode.PLAYING) {
            TimeGame = Time.time;
            // outras coisas
        }
        HandleInput();
	}

    void OnGUI() {
        
    }

    void HandleInput() {
        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.Escape)) {
                SwitchPause();
        }
        // temp
        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.E)) {
                EndGame();
        }
    }

    // Game End
    public void EndGame() {
        // apurar condicoes de vitoria       

        // melhores jogadores
        Players = new SortedDictionary<string, PlayerScore>();
        ReadPlayersFromFile(Definitions.PLAYERS_FILE);
        AddCurrentPlayerToBestScores();
        WritePlayersToFile(Definitions.PLAYERS_FILE);

        // parar o timer

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

    // Pause Game
    public void SwitchPause() {
        Debug.Log("entrei");
        if (CurrentMode == Definitions.GameMode.PLAYING) {
            Debug.Log("Ola");
            CurrentMode = Definitions.GameMode.PAUSE;
            
        }
        else {
            Debug.Log("Adeus");
            CurrentMode = Definitions.GameMode.PLAYING;
        }
    }

    // Current Player
    public void SetCurrentPlayer(string playerName) {
        CurrentPlayerScore = new PlayerScore(playerName, 0);

        // cheating - MUAHAHAHAHAHAH
        CurrentPlayerScore.Score += 4;
    }

    public void AddCurrentPlayerToBestScores() {
        PlayerScore ps;

        if (Players.TryGetValue(CurrentPlayerScore.Name, out ps)) {
            if (CurrentPlayerScore.Score > ps.Score) {
                ps.Score = CurrentPlayerScore.Score;
            }
        }
        else Players.Add(CurrentPlayerScore.Name, CurrentPlayerScore);
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

            if (name.Equals(CurrentPlayerScore.Name)) {
                HasCurrentPlayer = true;
            }
        }


       if(!HasCurrentPlayer) {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "player", null);

            XmlNode nodeName = document.CreateElement("name");

            nodeName.InnerText = CurrentPlayerScore.Name;

            XmlNode nodeScore = document.CreateElement("score");
            nodeScore.InnerText = "" + CurrentPlayerScore.Score;

            newNode.AppendChild(nodeName);
            newNode.AppendChild(nodeScore);

            document["players"].AppendChild(newNode);
        }
        document.Save(Definitions.PLAYERS_FILE);
    }

}
