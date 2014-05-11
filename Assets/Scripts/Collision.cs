using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag.Equals("Box"))
            // Destroy(hit.gameObject);
            hit.gameObject.rigidbody.AddForce(hit.moveDirection * 5, ForceMode.Impulse);

       // if (Definitions.Debug) Debug.Log("Colidi");
    }

}
