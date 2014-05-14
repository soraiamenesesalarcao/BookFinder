using UnityEngine;
using System.Collections;

public class CollisionBullet : MonoBehaviour {

    public bool shoot = false;
    private int audioInfo = 0;

	void Start(){
        gameObject.transform.FindChild("ShockFlame").particleSystem.Stop();
        gameObject.transform.FindChild("ShockFlame").FindChild("Flame").particleSystem.Stop();
    }

    void Update() {
        if (shoot) {
            if (audioInfo == 0) { 
                audio.Play();
                audioInfo++;
            }
            gameObject.transform.Translate(transform.forward * Time.deltaTime * 5f);
        }
        else {
            if (gameObject.transform.FindChild("ShockFlame").particleSystem.isStopped)
                //Destroy(gameObject);
                Debug.Log("é para destruir");
        }
    }

    void OnTriggerStay(Collider c) {
        if (shoot == true && (c.CompareTag("Player") || c.CompareTag("Walls"))) {
            gameObject.transform.FindChild("ShockFlame").particleSystem.Play();
            gameObject.transform.FindChild("ShockFlame").FindChild("Flame").particleSystem.Play();
            shoot = false;

            if (c.CompareTag("Player")) {
                GameState.CurrentPlayer.Life -= Definitions.DAMAGE_ENEMY_BULLET;
                GameState.CurrentPlayer.NumberOfLives = GameState.CurrentPlayer.Life / Definitions.MAX_LIVES;
            }
        }
    }
}
