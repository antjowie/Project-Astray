﻿using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float mouseXSensitivity = 0.25f;
    [SerializeField] private float mouseYSensitivity = 0.5f;

    // All speed values are in seconds. For example, 50 pixels in a second
    [Header("Speed")]
    [SerializeField] private float forwardSpeed = 50f;
    [SerializeField] private float verticalSpeed = 25f;
    [SerializeField] private float horizontalSpeed = 50f;
    
    // We interpolate over this transform
    [Header("Rotation")]
    [SerializeField] private float rotationDamping = 0.2f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float rollCap = 360f;
    private Vector3 targetRotation = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Hold cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateShootState();
    }

    private void UpdateMovement()
    {
        // Update movement based on world space
        rb.MovePosition(rb.position +
            transform.forward * forwardSpeed * Input.GetAxis("forward") * Time.fixedDeltaTime +
            transform.right * horizontalSpeed * Input.GetAxis("horizontal") * Time.fixedDeltaTime +
            transform.up * verticalSpeed * Input.GetAxis("vertical") * Time.fixedDeltaTime);
    }

    private void UpdateRotation()
    {
        // We store the current delta mouse pos in a vector so we can easily use it
        Vector2 deltaMouse = new Vector2(
            Input.GetAxis("mouseX") * mouseXSensitivity,
            Input.GetAxis("mouseY") * mouseYSensitivity);
        float rollAmount = Input.GetAxis("roll") * rollCap * Time.fixedDeltaTime;
        
        // Update the target direction that the player want to face
        // Pitch Yaw Roll
        targetRotation.x += -deltaMouse.y;
        targetRotation.y += deltaMouse.x;
        targetRotation.z += rollAmount;

        // Apply rotation to local space and do it over time
        Vector3 rotation = Vector3.SmoothDamp(Vector3.zero, targetRotation, ref rotationVelocity, rotationDamping, rotationSpeed, Time.fixedDeltaTime);
        Quaternion nextRot = transform.rotation * Quaternion.Euler(rotation);
        targetRotation -= rotation;

        Quaternion oldRot = rb.rotation;
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, nextRot, rotationSpeed * Time.fixedDeltaTime));

        // Some debug rays to show intended direction
        Debug.DrawRay(transform.position, transform.forward * 10);
        Debug.DrawRay(transform.position, transform.rotation * Quaternion.Euler(targetRotation) * Vector3.forward * 10, Color.green);
    }
    
    private void UpdateShootState()
    {
        // Shoot if player wants to 
        if (Input.GetAxisRaw("fire") == 1)
        {
            // Should be an interface
            transform.Find("Weapon").GetComponent<AutomaticWeapon>().Shoot();
        }
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateRotation();
    }
}