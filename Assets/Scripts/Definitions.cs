
public static class Definitions {

    // Convencao: CAPS_LOCK para const, Iniciais em Maiuscula para restantes

    // Modes
    public static bool Debug = true;

    // Game State
    public enum Levels : int { START, GAME, END };
    public enum GameMode : int { PLAYING, PAUSE };
    public const float DURATION = 300;
    public const float EVENT_DELTA = 5;

    // Values

    public const int MIN_ORANGE = 1;
    public const int MIN_YELLOW = 1;
    public const int MIN_BORDEAUX = 1;
    public const int MIN_BLUE = 1;

    public const int SCORE_ORANGE = 5; 
    public const int SCORE_YELLOW = 10;
    public const int SCORE_BORDEAUX = 15;
    public const int SCORE_BLUE = 20;
    public const int SCORE_COIN = 10;
    public const int COIN_AMOUNT = 10;

    public const int MAX_LIVES = 3;
    public const int MAX_LIFE = 99;

    public const int DAMAGE_ENEMY_BULLET = 1;

    // End Screen
    public const int MAX_BEST_PLAYERS = 10;

    // External Files
    public const string PLAYERS_FILE = "Assets/Resources/Config/Players.xml";


}
