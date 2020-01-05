﻿using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;

    [SerializeField] private float mouseXSensitivity = 1;
    [SerializeField] private float mouseYSensitivity = 1;

    [SerializeField] private float forwardSpeed = 50;
    [SerializeField] private float verticalSpeed = 25;
    [SerializeField] private float horizontalSpeed = 50;

    [SerializeField] private float rollSpeed = 360;
    
    [SerializeField] private float shootCooldown = 0.1f;

    // We keep target rotation in Euler Angles so that we can easily modify it
    [SerializeField]
    private Vector3 targetRotation;
    private bool canShoot = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Hold cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateMovement();
        UpdateShootState();

        if (Input.GetKey(KeyCode.P))
            targetRotation = Vector3.zero;
    }

    private void UpdateMovement()
    {
        // We store the current delta mouse pos in a vector so we can easily use it
        Vector2 deltaMouse = new Vector2(
            Input.GetAxis("mouseX") * mouseXSensitivity,
            Input.GetAxis("mouseY") * mouseYSensitivity);

        // Update the target direction that the player want to face
        // Pitch Yaw Roll
        Vector3 deltaRotation = new Vector3();

        //deltaRotation.x += -Input.GetAxis("roll") * rollSpeed * Time.deltaTime;
        //deltaRotation.x += Time.deltaTime * rollSpeed;
        deltaRotation.x = -deltaMouse.y;
        deltaRotation.y = deltaMouse.x;
        deltaRotation.z = Input.GetAxis("roll") * rollSpeed * Time.deltaTime;
        //deltaRotation = transform.rotation * deltaRotation;

        //deltaRotation = transform.rotation * deltaRotation;
        targetRotation += deltaRotation;

        Debug.DrawRay(transform.position, Quaternion.Euler(targetRotation) * Vector3.forward * 10);

        // TEMP: Set rotation to player target rotation, this should happen overtime
        // and should roll the ship when the player 'flips' the ship
        transform.rotation = Quaternion.Euler(targetRotation);
        //targetRotation = transform.rotation.eulerAngles; // To make sure that the values stay in bounds
    }

    private void UpdateShootState()
    {
        // Shoot if player wants to 
        if (Input.GetAxisRaw("fire") == 1 && canShoot)
        {
            Invoke("MakeShootable", shootCooldown);
            canShoot = false;
            // TODO: Use a weapon script and call shoot on that instead of instantiating ourselves
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
    private void MakeShootable()
    {
        canShoot = true;
    }

    private void FixedUpdate()
    {
        // Create a vector to track all movement that we want to do this frame
        Vector3 movement = new Vector3();

        // Update movement in local space and rotate with world rotation
        movement.z = forwardSpeed * Input.GetAxis("forward"); 
        movement.x = horizontalSpeed * Input.GetAxis("horizontal");
        movement.y = verticalSpeed * Input.GetAxis("vertical");
        
        // NOTE: This may have to be transformed with the target transform instead of actual transform
        movement = transform.rotation * movement;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}