using UnityEngine;
using System.Collections;

public class CharacterInfo : MonoBehaviour {

    public string Name;
    public int NumberOfLives;
    public int Life;
    public int Score;
    public Inventory Items;
	
	void Start () {
        Items = new Inventory();
	}
	
	
	void Update () {
	
	}
}