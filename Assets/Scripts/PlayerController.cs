using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 inputVector;
    private float speed = 0.2f;


    void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x + inputVector.x * speed,
            transform.position.y ,
            transform.position.z + inputVector.y * speed
        );
    }

    public void HandleInteract(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Player Interact");
    }
    public void HandleMove(InputAction.CallbackContext callbackContext)
    {
        inputVector = callbackContext.ReadValue<Vector2>();
    }
    
}
