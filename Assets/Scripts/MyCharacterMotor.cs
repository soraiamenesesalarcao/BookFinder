﻿using UnityEngine;
using System.Collections;

public class MyCharacterMotor : MonoBehaviour {
 
    public float speed;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
 
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
         // We are grounded, so recalculate
         // move direction directly from axes
         moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
         moveDirection = transform.TransformDirection(moveDirection);
         moveDirection *= speed;
 
         if (Input.GetButton ("Jump")) {
         moveDirection.y = jumpSpeed;
         }
       } 
 
       // Apply gravity
       moveDirection.y -= gravity * Time.deltaTime;
 
       // Move the controller
       controller.Move(moveDirection * Time.deltaTime);
    }
 
 
}