using System;
using NUnit.Framework.Internal.Filters;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public InputActionReference TiltAction, Wings, left, right;
    public float TiltSpeed, WingsSpeed, jumpForce, horizontalSpeed;
    public float tiltValue, WingsValue;
    private bool leftPressed, rightPressed, bothPressed;
    private Rigidbody rb;
    private Animator _animator;
    public bool Collected = false;
    public float targetZAngle = 45f;
    public int count;
    public int circlesCount;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        
        Vector3 angledDirection = (transform.forward + Vector3.up * -0.5f).normalized;
        rb.AddForce(angledDirection * jumpForce, ForceMode.Impulse);

    }

    private bool justJumped = false;

    private void Update()
    {
        bothPressed = left.action.IsPressed() && right.action.IsPressed();

        WingsValue = Wings.action.ReadValue<float>();
        tiltValue = TiltAction.action.ReadValue<float>();
        

        if (!bothPressed)
        {
            float currentZ = transform.localEulerAngles.z;
            float targetZ = targetZAngle * WingsValue;

            if (currentZ > 180f) currentZ -= 360f;
            
            float newZ = Mathf.MoveTowards(currentZ, targetZ, WingsSpeed * Time.deltaTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
            transform.position += Vector3.right * horizontalSpeed * WingsValue * Time.deltaTime;
            
            _animator.SetBool("Jump", false);
            
            // transform.Rotate(Vector3.forward, WingsValue * WingsSpeed * Time.deltaTime);
        }

        if (bothPressed)
        {
            _animator.SetBool("Jump", true);
            
            Vector3 angledDirection = (transform.forward + Vector3.up * -0.25f).normalized;

            rb.AddForce(angledDirection * jumpForce, ForceMode.Impulse);

            justJumped = true;
        }

        transform.Rotate(Vector3.right, tiltValue * TiltSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (justJumped)
        {
            justJumped = false;
            return; // пропускаем перенаправление, чтобы сохранить импульс
        }
    
        Vector3 velocity = rb.linearVelocity;
    
        Vector3 vertical = Vector3.Project(velocity, Vector3.up);
        Vector3 horizontal = velocity - vertical;
        
        float dampingFactor = 0.9980f;
        horizontal *= Mathf.Pow(dampingFactor, Time.fixedDeltaTime * 160f);

    
        Vector3 newHorizontal = transform.forward * -1;
        newHorizontal.y = 0f;
        newHorizontal = newHorizontal.normalized * horizontal.magnitude;
    
        rb.linearVelocity = newHorizontal + vertical;
    }

    private void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
