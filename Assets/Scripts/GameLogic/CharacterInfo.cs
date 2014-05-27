using UnityEngine;
using System.Collections;

public class CharacterInfo {

    public string Name;
    public int NumberOfLives;
    public int Life;
    public int Energy;
    public int Score;
    public Inventory Items;
    public float TimePlayed;
    public bool HasShield;

    public CharacterInfo(string name) {
        Name = name;
        NumberOfLives = Definitions.MAX_LIVES;
        Life = Definitions.MAX_LIFE;
        Energy = Definitions.MAX_ENERGY/2;
        Score = 0;
        Items = new Inventory();
        TimePlayed = 0;
        HasShield = false;
    }	

	

}