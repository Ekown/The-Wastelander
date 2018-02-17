using System.Collections;
using UnityEngine;
using TeamUtility.IO;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    private PlayerMovement c_movement;  //Reference to PlayerMovement script
    private bool isJumping;             //To determine if the player is jumping

    private float h;	

	void Awake()
    {
        //References
        c_movement = GetComponent<PlayerMovement>();
	}
	
	void Update ()
    {
        ////If he is not jumping...
        //if (!isJumping)
        //{
        //    //See if button is pressed...
        //    isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        //}	
  
	}

    private void FixedUpdate()
    {
        //Get horizontal axis
        float h = InputManager.GetAxis("Horizontal");

        //if (Input.GetKey(InputManager.GM.Right))
        //    h = CrossPlatformInputManager.GetAxis("Horizontal");

        //Call movement function in PlayerMovement
        c_movement.Move(h, isJumping);
        //Reset
        isJumping = false;
    }
}
