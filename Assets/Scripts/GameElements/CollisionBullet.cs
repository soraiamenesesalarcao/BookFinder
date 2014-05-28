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

            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 bulletPosition = transform.position;

            Vector3 direction = (playerPosition - bulletPosition).normalized;

            if (gameObject.name.Contains("BulletTurret1") || gameObject.name.Contains("BulletTurret2") ||
                gameObject.name.Contains("BulletTurret3") || gameObject.name.Contains("BulletTurret4") ||
                gameObject.name.Contains("BulletSkeleton1") || gameObject.name.Contains("BulletSkeleton2") )
                    gameObject.transform.Translate(direction * Time.deltaTime * 15f);
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

            if (c.CompareTag("Player") && !GameState.CurrentPlayer.HasShield) {
                Debug.Log("Vou fazer dano!");
                GameState.CurrentPlayer.Life -= Definitions.DAMAGE_ENEMY_BULLET;
                int life = GameState.CurrentPlayer.Life;

                if (life <= 0) GameState.CurrentPlayer.NumberOfLives = 0;
                else if (life > 0 && life <= 34) GameState.CurrentPlayer.NumberOfLives = 1;
                else if (life > 34 && life < 67) GameState.CurrentPlayer.NumberOfLives = 2;
                else if (life >= 67) GameState.CurrentPlayer.NumberOfLives = 3;
            }
            if (c.CompareTag("Player") && GameState.CurrentPlayer.HasShield) {
                Debug.Log("NÃO Vou fazer dano!");
            }
        }
    }
}
