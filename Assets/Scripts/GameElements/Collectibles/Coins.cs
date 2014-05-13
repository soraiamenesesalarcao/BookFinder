using UnityEngine;
using System.Collections;

public class Coins : Collectible {

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag.Equals("Player")) {

            playerInfo = c.GetComponent(typeof(CharacterInfo)) as CharacterInfo;

            if (gameObject.tag.Equals("Coins")) {
                if (Definitions.Debug) Debug.Log("Coins");
                playerInfo.Items.coins++;

                if (Definitions.Debug) Debug.Log(playerInfo.Items.coins);
            }
        }
        GameObject.Destroy(gameObject);
    }

}
