using UnityEngine;
using System.Collections;

public class CharacterInfo {

    public string Name;
    public int NumberOfLives;
    public int Life;
    public int Score;
    public Inventory Items;

    public CharacterInfo(string name) {
        Name = name;
        NumberOfLives = Definitions.MAX_LIVES;
        Life = Definitions.MAX_LIFE;
        Score = 0;
        Items = new Inventory();
    }	
	
}