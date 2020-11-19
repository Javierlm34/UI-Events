using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    [Range(0, 300f)]
    private float sensitivity = 300f;

    [SerializeField]
    private Vector2 inputVector;

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void HandleLook (InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float mouseX = inputVector.x * sensitivity * Time.deltaTime;
        float mouseY = inputVector.y * sensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

       
    }
}
