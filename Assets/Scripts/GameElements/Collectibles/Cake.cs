using UnityEngine;
using System.Collections;

public class Cake : Collectible {


    void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag.Equals("Player")) {

            Debug.Log("Detectou o player");

            if (gameObject.tag.Equals("Cake")) {
                if (Definitions.Debug) Debug.Log("Energy");
                addEnergy(Definitions.ADD_ENERGY);
            }
            GameObject.Destroy(gameObject);
        }
    }

    protected void addEnergy(int contribution) {
        GameState.CurrentPlayer.Energy += contribution;
        if (GameState.CurrentPlayer.Energy > 100) GameState.CurrentPlayer.Energy = 100;
        if (GameState.CurrentPlayer.Energy < 0) GameState.CurrentPlayer.Energy = 0;
    }
}
