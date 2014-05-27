using UnityEngine;
using System.Collections;

public class Bullet : Collectible {


    void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag.Equals("Player")) {

            Debug.Log("Detectou o player");

            if (gameObject.tag.Equals("BulletSplit")) {
                if (Definitions.Debug) Debug.Log("Bullet Split");
                GameState.CurrentPlayer.Items.bullet_split += 3;

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.bullet_split);
            }

            if (gameObject.tag.Equals("BulletBucky")) {
                if (Definitions.Debug) Debug.Log("Bullet Bucky");
                GameState.CurrentPlayer.Items.bullet_bucky += 3;

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.bullet_bucky);
            }
            GameObject.Destroy(gameObject);
        }
        
    }
}
