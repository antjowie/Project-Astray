using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Vector3 targetRotation;

    [SerializeField] private float horizontalSensitivity = 1;
    [SerializeField] private float verticalSensitivity = 1;
    [SerializeField] private float horizontalSpeed = 50;
    [SerializeField] private float verticalSpeed = 50;

    private void Start()
    {
        // Hold cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 deltaMouse = new Vector2(
                             Input.GetAxis("mouseX") * horizontalSensitivity,
                             Input.GetAxis("mouseY") * verticalSensitivity);

        // roll yaw pitch
        targetRotation.z = Mathf.Clamp(targetRotation.z + deltaMouse.y,-90,90);
        targetRotation.y += deltaMouse.x;

        transform.rotation = Quaternion.Euler(targetRotation);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3();

        // Update movement in local space and rotate with world rotation
        movement.z = -horizontalSpeed * Input.GetAxis("horizontal");
        movement.x = verticalSpeed * Input.GetAxis("vertical");
        movement = transform.rotation * movement;

        transform.position += movement * Time.fixedDeltaTime;
    }
}