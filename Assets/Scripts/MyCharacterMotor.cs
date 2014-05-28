using UnityEngine;
using System.Collections;

public class MyCharacterMotor : MonoBehaviour {
 
    public float speed;
    public float jumpSpeed = 50.0F;
    public float gravity = -10.0F;

    public float incEnergy = 0;

    Vector3 moveDirection = Vector3.zero;
 
    new void Start () {
 
    }

    void Update() {
        CharacterController controller = gameObject.GetComponent<CharacterController>();

        if (GameState.CurrentPlayer.Energy <= 25) speed = 10.0f;
        else if (GameState.CurrentPlayer.Energy <= 50) speed = 15.0f;
        else if (GameState.CurrentPlayer.Energy <= 75) speed = 20.0f;
        else if (GameState.CurrentPlayer.Energy <= 100) speed = 25.0f;

        if (controller.isGrounded == true) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            if (incEnergy >= 1.0f) {
                GameState.CurrentPlayer.Energy -= 1;
                incEnergy = 0.0f;
            }
            else incEnergy += 0.01f;
        }
    }
 
 
}