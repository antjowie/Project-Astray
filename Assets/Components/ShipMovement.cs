using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float mouseXSensitivity = 1;
    [SerializeField] private float mouseYSensitivity = 1;

    [SerializeField] private float forwardSpeed = 50;
    [SerializeField] private float verticalSpeed = 25;
    [SerializeField] private float horizontalSpeed = 50;
    
    private Vector3 targetRotation;

    private void Start()
    {
        // Hold cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 deltaMouse = new Vector2(
            Input.GetAxis("mouseX") * mouseXSensitivity,
            Input.GetAxis("mouseY") * mouseYSensitivity);

        // Update the target direction that the player want to face
        // Roll Yaw Pitch
        targetRotation.z = Mathf.Clamp(targetRotation.z + deltaMouse.y,-90,90);
        targetRotation.y += deltaMouse.x;

        // TEMP: Set rotation to player target rotation, this should happen overtime
        // and should roll the ship when the player 'flips' the ship
        transform.rotation = Quaternion.Euler(targetRotation);
    }

    private void FixedUpdate()
    {
        // Create a vector to track all movement that we want to do this frame
        Vector3 movement = new Vector3();

        // Update movement in local space and rotate with world rotation
        movement.z = -horizontalSpeed * Input.GetAxis("horizontal");
        movement.x = forwardSpeed * Input.GetAxis("forward");
        movement.y = verticalSpeed * Input.GetAxis("vertical");
        // NOTE: This may have to be transformed with the target transform instead of actual transform
        movement = transform.rotation * movement;

        transform.position += movement * Time.fixedDeltaTime;
    }
}