using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

public class GameState : MonoBehaviour {

    public static GameState Instance;


    public static Definitions.Levels CurrentLevel;
    public Definitions.GameMode CurrentMode;

    public static SortedDictionary<string, PlayerScore> Players;
    public static CharacterInfo CurrentPlayer; 
    public static string NewPlayer;
    public static int LastScore;
    public static bool HasWon = true;

    public static float TimeGame { get; set; }

    private static float eventTime;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        TimeGame = 0;
        if (GameObject.Find("Molly") != null) 
            CurrentPlayer = GameObject.Find("Molly").GetComponent(typeof(CharacterInfo)) as CharacterInfo;
        if (CurrentPlayer != null) {
            CurrentPlayer.Name = NewPlayer;
            CurrentPlayer.Score = 0;
            if(Definitions.Debug) Debug.Log("Novo jogador: " + NewPlayer);
        }
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
        // temporario
        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.E)) {
              //  LastScore = CurrentPlayer.Score;
                EndGame();
        }
    }

    // Game End
    public void EndGame() {

        LastScore = CurrentPlayer.Score;
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
        
        GameObject map = GameObject.Find("MiniMap");
        GameObject player = GameObject.Find("Molly");
        CharacterMotor playerMotor;
        FPSInputController playerInput;
        MouseLook playerCameraX, playerCameraY;

        if (CurrentMode == Definitions.GameMode.PLAYING) {
            if(Definitions.Debug) Debug.Log("Pause on");

            // freeze game
            if (map != null) map.camera.enabled = false;
            if (player != null) {
                playerMotor = player.GetComponent(typeof(CharacterMotor)) as CharacterMotor;
                playerMotor.enabled = false;
                playerInput = player.GetComponent(typeof(FPSInputController)) as FPSInputController;
                playerInput.enabled = false;
                playerCameraY = player.transform.FindChild("Main Camera").GetComponent(typeof(MouseLook)) as MouseLook;
                playerCameraY.enabled = false;
                playerCameraX = player.GetComponent(typeof(MouseLook)) as MouseLook;
                playerCameraX.enabled = false;

            }
            // update mode
            CurrentMode = Definitions.GameMode.PAUSE;
            
        }
        else {
            if (Definitions.Debug) Debug.Log("Pause off");

            // unfreeze game
            if (map != null) map.camera.enabled = true;
            if (player != null) {
                playerMotor = player.GetComponent(typeof(CharacterMotor)) as CharacterMotor;
                playerMotor.enabled = true;
                playerInput = player.GetComponent(typeof(FPSInputController)) as FPSInputController;
                playerInput.enabled = true;
                playerCameraY = player.transform.FindChild("Main Camera").GetComponent(typeof(MouseLook)) as MouseLook;
                playerCameraY.enabled = true;
                playerCameraX = player.GetComponent(typeof(MouseLook)) as MouseLook;
                playerCameraX.enabled = true;
            }
            // update mode
            CurrentMode = Definitions.GameMode.PLAYING;
        }
    }

    // Current Player
    public void SetCurrentPlayer(string playerName) {
        NewPlayer = playerName;
    }

    public void AddCurrentPlayerToBestScores() {
        PlayerScore ps;

        //LastScore = CurrentPlayer.Score;

        if (Players.TryGetValue(CurrentPlayer.Name, out ps)) {
            if (CurrentPlayer.Score > ps.Score) {
                ps.Score = CurrentPlayer.Score;
            }
        }
        else {
            ps = new PlayerScore(CurrentPlayer.Name, CurrentPlayer.Score);
            Players.Add(CurrentPlayer.Name, ps);
        }
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
