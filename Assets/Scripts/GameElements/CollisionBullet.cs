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

            if (gameObject.name.Contains("BulletTurret1") || gameObject.name.Contains("BulletTurret2"))
                gameObject.transform.Translate(transform.forward * Time.deltaTime * 15f);
            if (gameObject.name.Contains("BulletTurret3"))
                gameObject.transform.Translate(-transform.right * Time.deltaTime * 15f);
            if (gameObject.name.Contains("BulletTurret4"))
                gameObject.transform.Translate(transform.right * Time.deltaTime * 15f);

        }
        else {
            if (gameObject.name.Contains("(Clone)") && 
                gameObject.transform.FindChild("ShockFlame").particleSystem.isStopped)
                Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider c) {
        if (shoot == true && (c.CompareTag("Player") || c.CompareTag("Walls"))) {
            gameObject.transform.FindChild("ShockFlame").particleSystem.Play();
            gameObject.transform.FindChild("ShockFlame").FindChild("Flame").particleSystem.Play();
            shoot = false;

            if (c.CompareTag("Player")) {
                GameState.CurrentPlayer.Life -= Definitions.DAMAGE_ENEMY_BULLET;
                int life = GameState.CurrentPlayer.Life;

                if (life == 0) GameState.CurrentPlayer.NumberOfLives = 0;
                else if (life > 0 && life <= 34) GameState.CurrentPlayer.NumberOfLives = 1;
                else if (life > 34 && life < 67) GameState.CurrentPlayer.NumberOfLives = 2;
                else if (life >= 67) GameState.CurrentPlayer.NumberOfLives = 3;
            }
        }
    }
}
