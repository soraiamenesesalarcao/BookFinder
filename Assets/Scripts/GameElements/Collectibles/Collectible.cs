using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    public int Points = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected void awardScore() {
        GameState.CurrentPlayer.Score += (int)(Points * (GameState.Instance.TimeRemaining() / 60.0f));
    }
}
