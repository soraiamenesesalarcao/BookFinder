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
//    public CharacterInfo CurrentPlayer; 


    public static bool HasWon = true;

    public static float TimeGame { get; set; }
    private float savedTimeScale = -1;

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
        if (CurrentLevel == Definitions.Levels.GAME) {
            if (CurrentMode == Definitions.GameMode.PLAYING) {
                TimeGame = Time.timeSinceLevelLoad;
                // outras coisas
            }
            HandleInput();
        }
	}

    void OnGUI() {
        
    }

    void HandleInput() {        
        
        if (CurrentLevel == Definitions.Levels.GAME 
            &&( Input.GetKeyUp(KeyCode.M) || TimeRemaining() <= 0)) { // ou o jogador saiu do labirinto

            if(TimeRemaining() <= 0)    
                EndGame(true);
            else EndGame(false);
        }

        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.Escape)) {
            SwitchPause();
        }

        // muito temporario
        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.Alpha1)) {
            CurrentPlayer.Score++; // incrementa de 2 em 2, wtf
        }
    }

    // Game End
    public void EndGame(bool timeup) {
        
        CheckVictory(timeup);

        CurrentPlayer.TimePlayed = TimeGame;

        FreezeGame(false);

        Players = new SortedDictionary<string, PlayerScore>();
        ReadPlayersFromFile(Definitions.PLAYERS_FILE);
        AddCurrentPlayerToBestScores();
        WritePlayersToFile(Definitions.PLAYERS_FILE);    

        ChangeLevel(Definitions.Levels.END);
    }

    // Game duration
    public float TimeRemaining() {
        float time = Definitions.DURATION - TimeGame;
        if (time > 0) 
            return time;
        else return 0;
    }

    // Victory
    void CheckVictory(bool timeup) {
        if (!timeup
            && CurrentPlayer.Items.book_blue >= Definitions.MIN_BLUE
            && CurrentPlayer.Items.book_bordeaux >= Definitions.MIN_BORDEAUX
            && CurrentPlayer.Items.book_orange >= Definitions.MIN_ORANGE
            && CurrentPlayer.Items.book_yellow >= Definitions.MIN_YELLOW)

            HasWon = true;
        else HasWon = false;
    }

    // Level change
    public void ChangeLevel(Definitions.Levels level) {
        CurrentLevel = level;
        Application.LoadLevel((int)level);
    }

    // Freeze game
    public void FreezeGame(bool freeze) {
        GameObject map = GameObject.Find("MiniMap");
        GameObject player = GameObject.Find("Molly");
        CharacterMotor playerMotor;
        FPSInputController playerInput;
        MouseLook playerCameraX, playerCameraY;

        if (freeze) {
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
            if (Time.timeScale != 0)
                savedTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else {
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
            if (savedTimeScale == 0)
                Time.timeScale = savedTimeScale;
            else
                Time.timeScale = 1;
        }
    }

    // Pause Game
    public void SwitchPause() {
        
        if (CurrentMode == Definitions.GameMode.PLAYING) {
            if(Definitions.Debug) Debug.Log("Pause on");                      
            // freeze game
            FreezeGame(true);
            // update mode
            CurrentMode = Definitions.GameMode.PAUSE;            
        }
        else {
            if (Definitions.Debug) Debug.Log("Pause off");
            // unfreeze game
            FreezeGame(false);
            // update mode
            CurrentMode = Definitions.GameMode.PLAYING;
        }
    }

    // Current Player
    public void SetCurrentPlayer(string playerName) {
        CurrentPlayer = new CharacterInfo(playerName);
        TimeGame = 0;
    }

    public void AddCurrentPlayerToBestScores() {
        PlayerScore ps;

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
