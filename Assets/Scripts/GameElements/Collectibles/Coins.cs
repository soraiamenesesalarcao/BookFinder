using UnityEngine;
using System.Collections;

public class Coins : Collectible {

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag.Equals("Player")) {

            if (gameObject.tag.Equals("Coins")) {
                if (Definitions.Debug) Debug.Log("Coins");
                GameState.CurrentPlayer.Items.coins += Definitions.COIN_AMOUNT;
                awardScore(Definitions.SCORE_COIN);

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.coins);
            }
            GameObject.Destroy(gameObject);
        }
        
    }

}
