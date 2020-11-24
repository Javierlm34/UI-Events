using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    private float speed = 0.2f;


    [SerializeField]
    [Range(0.1f, 10)]
    private float acceleration = 6f;
    [SerializeField]
    [Range(0.1f, 1)]
    private float maxSpeed = 40;
    [SerializeField]
    [Range(0, 1)]
    private float friccion = 0.9f;
    [SerializeField]
    [Range(0, 0.1f)]
    private float tolerance = 0.01f;

    [SerializeField]
    private CharacterController controller;
    private Vector2 inputVector;
    private Vector3 velocity;

    void FixedUpdate()
    {
        Vector3 direction = transform.right * inputVector.x + transform.forward * inputVector.y;
        velocity += direction * acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity *= friccion;
        if (velocity.magnitude < tolerance)
        {
            velocity = Vector3.zero;
        }
        else
        {
            controller.Move(velocity);
        }
    }

   
    public void HandleMove(InputAction.CallbackContext callbackContext)
    {
        inputVector = callbackContext.ReadValue<Vector2>();
    }

}

