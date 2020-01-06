using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float mouseXSensitivity = 0.25f;
    [SerializeField] private float mouseYSensitivity = 0.5f;

    // All speed values are in seconds. For example, 50 pixels in a second
    [SerializeField] private float forwardSpeed = 50f;
    [SerializeField] private float verticalSpeed = 25f;
    [SerializeField] private float horizontalSpeed = 50f;
    
    // TEMP: This should be controlled via a weapon script
    [SerializeField] private float shootCooldown = 0.1f;
    [SerializeField] private GameObject bulletPrefab = null;

    // We interpolate over this transform
    private Vector3 targetRotation = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;
    [SerializeField] private float rotationTime = 0.2f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float rollSpeed = 360f;

    private bool canShoot = true;
    private Rigidbody rb;
    private Transform cameraTrans;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTrans = transform.Find("Camera").transform;
        
        // Hold cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateRotation();
        UpdateShootState();
    }

    private void UpdateRotation()
    {
        // We store the current delta mouse pos in a vector so we can easily use it
        Vector2 deltaMouse = new Vector2(
            Input.GetAxis("mouseX") * mouseXSensitivity,
            Input.GetAxis("mouseY") * mouseYSensitivity);
        float rollAmount = Input.GetAxis("roll") * rollSpeed * Time.deltaTime;
        
        // Update the target direction that the player want to face
        // Pitch Yaw Roll
        targetRotation.x += -deltaMouse.y;
        targetRotation.y += deltaMouse.x;
        targetRotation.z += rollAmount;

        // Some debug rays to show intended direction
        Debug.DrawRay(transform.position, transform.forward * 10);
        Debug.DrawRay(transform.position, transform.rotation * Quaternion.Euler(targetRotation) * Vector3.forward * 10, Color.green);
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

        // Apply rotation to local space and do it over time
        var rotation = Vector3.SmoothDamp(Vector3.zero, targetRotation, ref rotationVelocity, rotationTime,float.MaxValue,Time.fixedDeltaTime);
        var nextRot = transform.rotation * Quaternion.Euler(rotation);
        targetRotation -= rotation;

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, nextRot, rotationSpeed * Time.fixedDeltaTime));
    }
}