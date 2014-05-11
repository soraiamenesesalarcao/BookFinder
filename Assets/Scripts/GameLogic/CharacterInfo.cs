using UnityEngine;
using System.Collections;

public class CharacterInfo : MonoBehaviour {


    public int NumberOfLives { get; set; }
    public int Score { get; set; }
    public ArrayList Inventory;
	
	void Start () {
        Inventory = new ArrayList();
	}
	
	
	void Update () {
	
	}
}