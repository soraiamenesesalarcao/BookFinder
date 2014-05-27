using UnityEngine;
using System.Collections;

public class CollisionSkeleton : MonoBehaviour {

    public bool continues = true;
    public bool player = false;
    public float moveSpeed = 15f;
    public RaycastHit hitInfo;
    protected System.Random random = new System.Random();
    public float time = 0;
    int timeElapsed = 0;
    Vector3 direction = Vector3.zero;

    public bool walk = true;
    public bool forward = true;

    public float timeUpdate = 0; 
	
    void Start() {
        if (gameObject.name.Equals("Skeleton1")) { direction = transform.right; }
        if (gameObject.name.Equals("Skeleton2")) { direction = transform.forward; }
        if (gameObject.name.Equals("Skeleton3")) { direction = transform.forward; }
        if (gameObject.name.Equals("Skeleton4")) { direction = transform.right; }
    }

	void Update () {

       if ((Time.time - time) >= 10) player = false; 

       if (!player) player = Obstacle();

       if (gameObject.name.Equals("Skeleton1")) {
           if (player) {
               gameObject.transform.Translate(Vector3.zero);
               gameObject.rigidbody.isKinematic = true;
               if (timeElapsed == 0 || timeElapsed >= 50) {
                   fire("BulletSkeleton1");
                   timeElapsed = 0;
               }
               timeElapsed++;
           }
           else {
               gameObject.rigidbody.isKinematic = false;
               gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed);
           }
       }
       if (gameObject.name.Equals("Skeleton2")) {
           if (player) {
               gameObject.transform.Translate(Vector3.zero);
               gameObject.rigidbody.isKinematic = true;
               if (timeElapsed == 0 || timeElapsed >= 50) {
                   fire("BulletSkeleton2");
                   timeElapsed = 0;
               }
               timeElapsed++;
           }
           else {
               gameObject.rigidbody.isKinematic = false;
               gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed);
           }
       }
       if (gameObject.name.Equals("Skeleton3")) {
           if (player) {
               gameObject.transform.Translate(Vector3.zero);
               gameObject.rigidbody.isKinematic = true;
               if (timeElapsed == 0 || timeElapsed >= 50) {
                   fire("BulletSkeleton3");
                   timeElapsed = 0;
               }
               timeElapsed++;
           }
           else {
               gameObject.rigidbody.isKinematic = false;
               gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed);
           }
       }
       if (gameObject.name.Equals("Skeleton4")) {
           if (player) {
               gameObject.transform.Translate(Vector3.zero);
               gameObject.rigidbody.isKinematic = true;
               if (timeElapsed == 0 || timeElapsed >= 50) {
                   fire("BulletSkeleton4");
                   timeElapsed = 0;
               }
               timeElapsed++;
           }
           else {
               gameObject.rigidbody.isKinematic = false;
               gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed);
           }
       }

	}

    void OnTriggerExit(Collider c){

        if ((Time.time - timeUpdate) >= 1) {

            if (gameObject.name.Equals("Skeleton1") && c.name.Equals("Hall8")) {
                if (forward) {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
                    direction = -transform.right;
                    forward = false;
                    timeUpdate = Time.time;
                }
                else {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180f); 
                    direction = transform.right;
                    forward = true;
                    timeUpdate = Time.time;
                }
            }
            if (gameObject.name.Equals("Skeleton2") && c.name.Equals("Hall1")) {
               if (forward) {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
                    direction = -transform.forward;
                    forward = false;
                    timeUpdate = Time.time;
               }
               else {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180f); 
                    direction = transform.forward;
                    forward = true;
                    timeUpdate = Time.time;
               }
            }
            if (gameObject.name.Equals("Skeleton3") && c.name.Equals("Hall4")) {
                if (forward) {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
                    direction = -transform.forward;
                    forward = false;
                    timeUpdate = Time.time;
                }
                else {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180f);
                    direction = transform.forward;
                    forward = true;
                    timeUpdate = Time.time;
                }
            }
            if (gameObject.name.Equals("Skeleton4") && c.name.Equals("Hall18")) {
                if (forward) {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
                    direction = -transform.right;
                    forward = false;
                    timeUpdate = Time.time;
                }
                else {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0), 180f);
                    direction = transform.right;
                    forward = true;
                    timeUpdate = Time.time;
                }
            }
        }
    }


    public bool Obstacle() {
        Vector3 direction = Quaternion.AngleAxis(random.Next(-45, 45), transform.up) * transform.forward;
        Vector3 initialPosition = transform.position;
        initialPosition.y -= 5;
        Vector3 finalPosition = initialPosition + direction * 50;
        finalPosition.y -= 5;

        if (Physics.Linecast(initialPosition, finalPosition, out hitInfo)) {
            if (hitInfo.collider.gameObject.tag.Equals("Player")) {
                time = Time.time;
                return true;
            }
            else return false;
        }
        //Debug.DrawLine(initialPosition, finalPosition, Color.red);
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
