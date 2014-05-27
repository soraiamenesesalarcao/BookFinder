using UnityEngine;
using System.Collections;

public class Shield : Collectible {


    void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag.Equals("Player")) {

            Debug.Log("Detectou o player");

            if (gameObject.tag.Equals("Shield")) {
                if (Definitions.Debug) Debug.Log("Shield");
                activateShield();
            }
            GameObject.Destroy(gameObject);
        }
    }

    protected void activateShield() {
        Debug.Log("Activei Shield");
        GameState.CurrentPlayer.HasShield = true;
        GameObject shield = GameObject.Find("Shield_Molly");
        shield.renderer.enabled = true;
        GameState.TimeShield = Time.time;
    }

    public void deactivateShield() {
        Debug.Log("Desactivei Shield");
        GameState.CurrentPlayer.HasShield = false;
        GameObject shield = GameObject.Find("Shield_Molly");
        shield.renderer.enabled = false;
    }
}
