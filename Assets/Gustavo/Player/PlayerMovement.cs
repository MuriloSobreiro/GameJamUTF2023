using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController playerController;
    public float movSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * movSpeed;
    }

    void FixedUpdate()
    {
        playerController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);        
    }
}
