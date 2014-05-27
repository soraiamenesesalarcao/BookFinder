using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class GameState : MonoBehaviour {

    public static GameState Instance;


    public static Definitions.Levels CurrentLevel;
    public Definitions.GameMode CurrentMode;
    public bool VisibleInventory = false;

    public static SortedDictionary<string, PlayerScore> Players;
    public static SortedList<string, PlayerScore> TopPlayers;
    public static CharacterInfo CurrentPlayer; 

    public static bool HasWon = true;

    public static float TimeGame { get; set; }
    public static float TimeShield { get; set; }
    private float savedTimeScale = -1;

    private System.Random rndPositions = new System.Random();
    private Vector3[] positions;
    private ArrayList usedPositions;



    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        TimeGame = Time.time;
        if (CurrentLevel == Definitions.Levels.GAME) {
            usedPositions = new ArrayList();
            InitPositions();
            DestroyBooks();
            DestroyCakes();
            //DestroyBullets();
            DestroyShields();
            GenerateBooks();
            GenerateCakes();
            //GenerateBullets();
            GenerateShields();        }
    }

    void Start() {
        CurrentMode = Definitions.GameMode.PLAYING;
        GameObject shield = GameObject.Find("Shield_Molly");
        Shield s = shield.GetComponent<Shield>() as Shield;
        s.deactivateShield();
    }
    	
	// Update is called once per frame
	void Update () {
        if (CurrentLevel == Definitions.Levels.GAME) {
            if (CurrentMode == Definitions.GameMode.PLAYING) {
                TimeGame = Time.timeSinceLevelLoad;

                // Shield
                if (CurrentPlayer.HasShield && (Time.time - TimeShield >= 10.0f)) {
                    GameObject shield = GameObject.Find("Shield_Molly");
                    Shield s = shield.GetComponent<Shield>() as Shield;
                    s.deactivateShield();
                    TimeShield = 0;
                }
            }
            HandleInput();
        }
	}

    void OnGUI() {
        
    }

    void HandleInput() {        
        
        if (CurrentLevel == Definitions.Levels.GAME 
            &&( TimeRemaining() <= 0
                || CurrentPlayer.Life <= 0)) {

            if(TimeRemaining() <= 0)    
                EndGame(true);
            else EndGame(false);
        }

        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.Escape)) {
            SwitchPause();
        }

        if (CurrentLevel == Definitions.Levels.GAME && Input.GetKeyUp(KeyCode.I)) {
            VisibleInventory = (VisibleInventory) ? false : true;
        }

    }

    // Collectibles init

    public void InitPositions() {

        positions = new Vector3[Definitions.COLLECTIBLE_POSITIONS];        

        positions[0] = new Vector3(120, -165, 120);
        positions[1] = new Vector3(10, -165, 85);
        positions[2] = new Vector3(0, -165, -75);
        positions[3] = new Vector3(115, -165, 10);
        positions[4] = new Vector3(80, -165, -75);
        positions[5] = new Vector3(80, -165, -115);
        positions[6] = new Vector3(-20, -165, 15);
        positions[7] = new Vector3(30, -165, 0);
        positions[8] = new Vector3(20, -165, -25);
        positions[9] = new Vector3(40, -165, 120);

        positions[10] = new Vector3(0, -165, 20);
        positions[11] = new Vector3(60, -165, 80);
        positions[12] = new Vector3(60, -165, 105);
        positions[13] = new Vector3(0, -165, 140);
        positions[14] = new Vector3(-40, -165, 70);
        positions[15] = new Vector3(-70, -165, 105);
        positions[16] = new Vector3(-110, -165, 140);
        positions[17] = new Vector3(-120, -165, 80);
        positions[18] = new Vector3(-100, -165, 90);
        positions[19] = new Vector3(-60, -165, -120);

        positions[20] = new Vector3(-140, -165, -75);
        positions[21] = new Vector3(-55, -165, -75);
        positions[22] = new Vector3(-95, -165, -35);
        positions[23] = new Vector3(-140, -165, 0);
        positions[24] = new Vector3(-120, -165, 30);
        positions[25] = new Vector3(-100, -165, 50);
        positions[26] = new Vector3(-80, -165, 25);
        positions[27] = new Vector3(-60, -165, 0);
    }


    public void DestroyBooks() {
        GameObject[] books_orange = GameObject.FindGameObjectsWithTag("BookOrange");
        GameObject[] books_bordeaux = GameObject.FindGameObjectsWithTag("BookBordeaux");
        GameObject[] books_blue = GameObject.FindGameObjectsWithTag("BookBlue");
        GameObject[] books_yellow = GameObject.FindGameObjectsWithTag("BookYellow");
        foreach (GameObject bo in books_orange) {
            Destroy(bo);
        }
        foreach (GameObject bbo in books_bordeaux) {
            Destroy(bbo);
        }
        foreach (GameObject bbl in books_blue) {
            Destroy(bbl);
        }
        foreach (GameObject by in books_yellow) {
            Destroy(by);
        }
    }

    public void DestroyCakes() {
        GameObject[] cakes = GameObject.FindGameObjectsWithTag("Cake");
        foreach (GameObject ca in cakes) {
            Destroy(ca);
        }
    }

    public void DestroyShields() {
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
        foreach (GameObject sh in shields) {
            Destroy(sh);
        }
    }

    public void DestroyBullets() {
        GameObject[] bullets_split = GameObject.FindGameObjectsWithTag("BulletSplit");
        GameObject[] bullets_bucky = GameObject.FindGameObjectsWithTag("BulletBucky");
        foreach (GameObject bs in bullets_split) {
            Destroy(bs);
        }
        foreach (GameObject bc in bullets_bucky) {
            Destroy(bc);
        }
    }

    public void GenerateBooks() {
        GameObject[] books_blue = new GameObject[Definitions.MAX_BLUE];
        GameObject[] books_bordeaux = new GameObject[Definitions.MAX_BORDEAUX];
        GameObject[] books_orange = new GameObject[Definitions.MAX_ORANGE];
        GameObject[] books_yellow = new GameObject[Definitions.MAX_YELLOW];
        int rp;

        for (int i = 0; i < Definitions.MAX_BLUE; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);                
            } while(usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            books_blue[i] = Instantiate(GameObject.Find("Book_Blue"), positions[rp], new Quaternion()) as GameObject;
        }

        for (int i = 0; i < Definitions.MAX_BORDEAUX; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
                        usedPositions.Add(positions[rp]);
            books_bordeaux[i] = Instantiate(GameObject.Find("Book_Bordeaux"), positions[rp], new Quaternion()) as GameObject;
        }

        for (int i = 0; i < Definitions.MAX_ORANGE; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            books_orange[i] = Instantiate(GameObject.Find("Book_Orange"), positions[rp], new Quaternion()) as GameObject;
        }

        for (int i = 0; i < Definitions.MAX_YELLOW; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            books_yellow[i] = Instantiate(GameObject.Find("Book_Yellow"), positions[rp], new Quaternion()) as GameObject;
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos) {
            if (go.name.Contains("Book")) {
                go.light.enabled = true;
            }
        }
       
    }

    public void GenerateCakes() {
        GameObject[] cakes = new GameObject[Definitions.MAX_CAKES];
        int rp;

        for (int i = 0; i < Definitions.MAX_CAKES; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            cakes[i] = Instantiate(GameObject.Find("Cake"), positions[rp], new Quaternion()) as GameObject;
        }

        /*GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos) {
            if (go.name.Contains("Book")) {
                go.light.enabled = true;
            }
        }*/

    }

    public void GenerateShields() {
        GameObject[] shields = new GameObject[Definitions.MAX_SHIELD];
        int rp;

        for (int i = 0; i < Definitions.MAX_SHIELD; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            shields[i] = Instantiate(GameObject.Find("Shield"), positions[rp], new Quaternion()) as GameObject;
            shields[i].transform.Rotate(new Vector3(1, 0, 0), -90);
            shields[i].transform.Rotate(new Vector3(0, 0, 1), 90);
        }

        /*GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos) {
            if (go.name.Contains("Book")) {
                go.light.enabled = true;
            }
        }*/
    }

    public void GenerateBullets() {
        GameObject[] bullet_splits = new GameObject[Definitions.MAX_SPLIT];
        GameObject[] bullet_buckys = new GameObject[Definitions.MAX_BUCKY];
        int rp;

        for (int i = 0; i < Definitions.MAX_SPLIT; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            bullet_splits[i] = Instantiate(GameObject.Find("Bullet_Split"), positions[rp], new Quaternion()) as GameObject;
        }

        for (int i = 0; i < Definitions.MAX_BUCKY; i++) {
            do {
                rp = rndPositions.Next(0, Definitions.COLLECTIBLE_POSITIONS);
            } while (usedPositions.Contains(positions[rp]));
            usedPositions.Add(positions[rp]);
            bullet_buckys[i] = Instantiate(GameObject.Find("Bullet_Bucky"), positions[rp], new Quaternion()) as GameObject;
        }

        /*GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");
foreach (GameObject go in gos) {
    if (go.name.Contains("Book")) {
        go.light.enabled = true;
    }
}*/
    
    }

    // Game End
    public void EndGame(bool timeup) {
        
        CheckVictory(timeup);

        CurrentPlayer.TimePlayed = TimeGame;

        FreezeGame(false);

        VisibleInventory = false;

        Players = new SortedDictionary<string, PlayerScore>();
        //ReadPlayersFromFile(Definitions.PLAYERS_FILE);
        ReadPlayersFromFile(Definitions.PLAYERS_FILE_SA);
        if(HasWon) AddCurrentPlayerToBestScores();
        //WritePlayersToFile(Definitions.PLAYERS_FILE);
        WritePlayersToFile(Definitions.PLAYERS_FILE_SA);

        SortTopPlayers();

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
        MyCharacterMotor playerMotor;
        FPSInputController playerInput;
        MouseLook playerCameraX, playerCameraY;

        if (freeze) {
            if (map != null) map.camera.enabled = false;
            if (player != null) {
                playerMotor = player.GetComponent(typeof(MyCharacterMotor)) as MyCharacterMotor;
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
                playerMotor = player.GetComponent(typeof(MyCharacterMotor)) as MyCharacterMotor;
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
            FreezeGame(true);
            CurrentMode = Definitions.GameMode.PAUSE;            
        }
        else {
            if (Definitions.Debug) Debug.Log("Pause off");
            FreezeGame(false);
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

    void SortTopPlayers() {
        //tpComparer = new TopPlayersComparer(Players);
        //TopPlayers = tpComparer.list;
        //Debug.Log("jogador: " + TopPlayers.Values[0]);
    }

    void ReadPlayersFromFile(string filename) {
        string name;
        int score;
        PlayerScore ps;

        TextAsset XMLFile = (TextAsset)Resources.Load(filename);

        XmlDocument document = new XmlDocument();
        document.LoadXml(XMLFile.text);
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

        TextAsset xmlData = Resources.Load(filename) as TextAsset;
        XmlDocument document = new XmlDocument();
        document.LoadXml(xmlData.text);
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
        //document.Save(Definitions.PLAYERS_FILE);
       //document.Save(Definitions.PLAYERS_FILE_SA);
        #if UNITY_EDITOR
       document.Save(AssetDatabase.GetAssetPath(xmlData));
    #endif
    }

}
