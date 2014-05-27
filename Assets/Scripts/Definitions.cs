
public static class Definitions {

    // Convencao: CAPS_LOCK para const, Iniciais em Maiuscula para restantes

    // Modes
    public static bool Debug = true;

    // Game State
    public enum Levels : int { START, GAME, END };
    public enum GameMode : int { PLAYING, PAUSE };
    public const float DURATION = 300;

    // Values
    public const int COLLECTIBLE_POSITIONS = 28;

    public const int MIN_ORANGE = 1;
    public const int MIN_YELLOW = 1;
    public const int MIN_BORDEAUX = 1;
    public const int MIN_BLUE = 1;

    public const int MAX_ORANGE = 4;
    public const int MAX_YELLOW = 3;
    public const int MAX_BORDEAUX = 2;
    public const int MAX_BLUE = 1;

    public const int SCORE_ORANGE = 5; 
    public const int SCORE_YELLOW = 10;
    public const int SCORE_BORDEAUX = 15;
    public const int SCORE_BLUE = 20;

    public const int MAX_BUCKY = 4;
    public const int MAX_SPLIT = 2;
    public const int MAX_CAKES = 3;
    public const int MAX_SHIELD = 5;

    public const int MAX_LIVES = 3;
    public const int MAX_LIFE = 99;
    public const int MAX_ENERGY = 100;

    public const int ADD_ENERGY = 15;
    public const int TIMER_SHIELD = 10; //seconds

    public const int DAMAGE_ENEMY_BULLET = 5;
    public const int DAMAGE_MOLLY_BUCKY = 5;
    public const int DAMAGE_MOLLY_SPLIT = 10;

    // End Screen
    public const int MAX_BEST_PLAYERS = 10;

    // External Files
    //public const string PLAYERS_FILE = "Assets/Resources/Config/Players.xml";
    public const string PLAYERS_FILE_SA = "Config/Players";


}
