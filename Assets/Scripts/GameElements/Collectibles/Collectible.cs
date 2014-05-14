using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    protected void awardScore(int contribution) {
        GameState.CurrentPlayer.Score += (int)(contribution * (GameState.Instance.TimeRemaining() / 60.0f));
    }
}
