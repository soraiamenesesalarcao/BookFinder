
public static class Definitions {

    // Convencao: CAPS_LOCK para const, Iniciais em Maiuscula para restantes

    // Modes
    public static bool Debug = true;

    // Game State
    public enum Levels : int { START, GAME, END };
    public enum GameMode : int { PLAYING, PAUSE };

    // End Screen
    public const int MAX_BEST_PLAYERS = 10;

    // External Files
    public const string PLAYERS_FILE = "Assets/Resources/Config/Players.xml";


}
