using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;
    [SerializeField] private float rotationSpeed = 500f;

    void Update()
    {   
        //cache input values in floats
        float horizonInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //create movement direction vector3 and store the vertical and horizontal values in it
        Vector3 movementDirection = new Vector3(horizonInput, 0, verticalInput);
        movementDirection.Normalize();

        //moves the transform in movement direction
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        //Rotate the player to face the movement direction
        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
