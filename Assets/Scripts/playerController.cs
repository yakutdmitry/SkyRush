using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public InputActionReference TiltAction, Wings, left, right;
    // public InputActionReference Wings;
    public float TiltSpeed, WingsSpeed = 100f;
    public float tiltValue, WingsValue;
    [SerializeField] private bool leftPressed, rightPressed, bothPressed;
    
    
    
    private void Update()
    {

        bothPressed = left.action.IsPressed() && right.action.IsPressed();
        
        if (!bothPressed)
        {
            transform.Rotate(Vector3.forward, WingsValue * WingsSpeed * Time.deltaTime);
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
