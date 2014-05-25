using UnityEngine;
using System.Collections;

public class CollisionSkeleton : MonoBehaviour {

    public bool continues = false;
    public bool player = false;
    public float moveSpeed = 15f;
    public RaycastHit hitInfo;
    protected System.Random random = new System.Random();
    public float time = 0;
    int timeElapsed = 0;
	
	// Update is called once per frame
	void Update () {

       if ((Time.time - time) >= 5) player = false;

       if(!player) player = Obstacle();

        if (gameObject.name.Contains("Skeleton2") || gameObject.name.Contains("Skeleton3")) {
            if (player) {
                gameObject.transform.Translate(transform.forward * Time.deltaTime * 0);
                if (gameObject.name.Contains("Skeleton2") && (timeElapsed == 0 || timeElapsed >= 50)) { 
                    fire("BulletSkeleton2");
                    timeElapsed = 0;
                }
                if (gameObject.name.Contains("Skeleton3") && (timeElapsed == 0 || timeElapsed >= 50)) {
                    fire("BulletSkeleton3");
                    timeElapsed = 0;
                }
                timeElapsed++;   
            }
            else {
                if (continues) gameObject.transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
                else gameObject.transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);
            }
        }

        if (gameObject.name.Contains("Skeleton1") || gameObject.name.Contains("Skeleton4")) {
            if (player) {
                gameObject.transform.Translate(transform.forward * Time.deltaTime * 0);
                if (gameObject.name.Contains("Skeleton1") && (timeElapsed == 0 || timeElapsed >= 50)) {
                    fire("BulletSkeleton1");
                    timeElapsed = 0;
                }
                if (gameObject.name.Contains("Skeleton4") && (timeElapsed == 0 || timeElapsed >= 50)) {
                    fire("BulletSkeleton4");
                    timeElapsed = 0;
                }
                timeElapsed++; 
            }
            else {
                if (continues) gameObject.transform.Translate(transform.right * Time.deltaTime * moveSpeed);
                else gameObject.transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
            }
        }
	}

    void OnTriggerEnter(Collider c){
        if(c.CompareTag("Player")){
            Debug.Log("cenaz");
            if(continues) continues = false;
            else continues = true;
        }

        if (gameObject.name.Contains("Skeleton1")){
            if (c.CompareTag("HallS"))  continues = true;
            if (c.name.Equals("Hall4")) continues = false;
        }

        if (gameObject.name.Contains("Skeleton2")){
            if (c.CompareTag("HallS")) continues = true;
            if(c.name.Equals("Room1")) continues = false;
       }

        if (gameObject.name.Contains("Skeleton3")){
            if (c.name.Equals("Room3")) continues = true;
            if (c.name.Equals("Room2")) continues = false;
        }

        if (gameObject.name.Contains("Skeleton4")) {
            if (c.name.Equals("Hall19")) continues = false;
            if (c.name.Equals("Hall13")) continues = true;
        }
    }

    void OnTriggerExit(Collider c){
        if (c.CompareTag("HallS") || c.name.Equals("Room1") || c.name.Equals("Room2") || c.name.Equals("Room3") || c.name.Equals("Hall8") ||
            c.name.Equals("Hall19") || c.name.Equals("Hall13") || c.name.Equals("Hall4")) {
            
            gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
        }
        if (c.CompareTag("Player")) player = false;
    }

    public bool Obstacle() {
        Vector3 direction = Quaternion.AngleAxis(random.Next(-30, 30), transform.up) * transform.forward;
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = initialPosition + direction * 75;

        if (Physics.Linecast(initialPosition, finalPosition, out hitInfo)) {
            if (hitInfo.collider.gameObject.tag.Equals("Player")) {
                time = Time.time;
                return true;
            }
            else return false;
        }
        Debug.DrawLine(initialPosition, finalPosition, Color.red);
        return false;
    }
    void fire(string obj) {
        GameObject go = GameObject.Find(obj);
        GameObject bullet = Instantiate(go, go.transform.position, Quaternion.identity) as GameObject;
        bullet.transform.localScale = new Vector3(1, 1, 1);
        CollisionBullet cb = bullet.GetComponent("CollisionBullet") as CollisionBullet;
        cb.shoot = true;
    }
}
