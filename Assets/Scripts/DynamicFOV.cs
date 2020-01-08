using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speedThreshold = 1f;
    [SerializeField] private float FOVChangeTime = 1f;

    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float maxFOV = 90f;
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    private Vector3 oldPos;
    private float FOVProgress;

    // For debugging purposes
    float speed;
    float FOV;

    void Awake()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();    
    }

    void Start()
    {
        oldPos = playerTransform.position;
    }

    void FixedUpdate()
    {
        // Calculate forward speed
        Vector3 forwardMovement = playerTransform.position - oldPos;
        speed = Vector3.Project(forwardMovement,playerTransform.forward).magnitude;
        if (Vector3.Dot(forwardMovement, playerTransform.forward) < 0f) // We only want to apply if player moves forward
            speed = 0f;

        // Calculate new progression value
        float progress = speed > speedThreshold ? Time.fixedDeltaTime : -Time.fixedDeltaTime;
        progress /= FOVChangeTime;
        FOVProgress = Mathf.Clamp(FOVProgress + progress, 0, 1);

        // Apply new FOV
        FOV = Mathf.SmoothStep(minFOV, maxFOV, FOVProgress);
        virtualCamera.m_Lens.FieldOfView = FOV;

        oldPos = playerTransform.position;
    }
}
