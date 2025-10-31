using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public InputActionReference TiltAction, Wings, left, right;
    // public InputActionReference Wings;
    public float TiltSpeed, WingsSpeed, jumpForce, jumpAngleZ;
    public float tiltValue, WingsValue;
    [SerializeField] private bool leftPressed, rightPressed, bothPressed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        bothPressed = left.action.IsPressed() && right.action.IsPressed();
        
        if (!bothPressed)
        {
            transform.Rotate(Vector3.forward, WingsValue * WingsSpeed * Time.deltaTime);
        }

        if (bothPressed)
        {
            Vector3 localDirection = new Vector3(0, 1, jumpAngleZ).normalized;
            Vector3 WorldDirection = transform.TransformDirection(localDirection);
            rb.AddForce(WorldDirection * jumpForce, ForceMode.Impulse);
        }

        WingsValue = Wings.action.ReadValue<float>();
        tiltValue = TiltAction.action.ReadValue<float>();
        transform.Rotate(Vector3.right , tiltValue * TiltSpeed * Time.deltaTime);
        
        
    }

    // private void OnEnable()
    // {
    //     Wings.action.performed += _Wings;
    // }
    //
    // private void OnDisable()
    // {
    //     Wings.action.performed -= _Wings;
    // }
    //
    // private void _Wings(InputAction.CallbackContext context)
    // {
    //     throw new NullReferenceException();
    // }
}
