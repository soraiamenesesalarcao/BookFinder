using UnityEngine;
using System.Collections;

public class CollisionSkeleton : MonoBehaviour {

    public bool continueS = false;
    public bool player = false;
    public float moveSpeed = 20f;
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name.Contains("Skeleton2") || gameObject.name.Contains("Skeleton3")) {
            if (continueS) {
                gameObject.transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
            }
            else {
                gameObject.transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);
            }
        }

        if (gameObject.name.Contains("Skeleton1") || gameObject.name.Contains("Skeleton4")) {
            if (continueS) {
                gameObject.transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            }
            else {
                gameObject.transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
            }
        }
	}

    void OnTriggerEnter(Collider c){
        if(gameObject.CompareTag("Player")){
            if(continueS)
                continueS = false;
            else
                continueS = true;
        }

        if (gameObject.name.Contains("Skeleton1")){
            if (c.CompareTag("HallS"))
                continueS = true;
           
            if(c.name.Equals("Hall12")) 
                continueS = false;
        }

        if (gameObject.name.Contains("Skeleton2")){
            if (c.CompareTag("HallS")){
                continueS = true;
            }
            if(c.name.Equals("Room1")){
                   continueS = false;
           }
       }

        if (gameObject.name.Contains("Skeleton3")){
            if (c.name.Equals("Room3")) {
                continueS = true;
            }
            if (c.name.Equals("Room2")) {
                continueS = false;
            }
        }

        if (gameObject.name.Contains("Skeleton4")) {
            if (c.name.Equals("Hall19")){
                continueS = false;
            }
            if (c.name.Equals("Hall13")){
                continueS = true;
            }
        }
    }

    void OnTriggerExit(Collider c){
        if (c.CompareTag("HallS") || c.name.Equals("Room1") || c.name.Equals("Room2") || c.name.Equals("Room3")
            || c.name.Equals("Hall19") || c.name.Equals("Hall13") || c.name.Equals("Hall12") || gameObject.CompareTag("Player")) {
            
            gameObject.transform.Rotate(new Vector3(0, 1, 0), 180.0f);
        }
    }
}
