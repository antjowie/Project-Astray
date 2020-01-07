using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speedThreshold = 50f;
    [SerializeField] private float speedProgression = 10f;
    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float maxFOV = 90f;
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    private float oldZ;

    // For debugging purposes
    float speed;
    float FOV;

    void Awake()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();    
    }

    void Start()
    {
        oldZ = playerTransform.position.z;
    }

    void LateUpdate()
    {
        speed = (playerTransform.position - oldPos).magnitude;
        FOV = Mathf.SmoothStep(minFOV, maxFOV, (speed - speedThreshold) / speedProgression);
        virtualCamera.m_Lens.FieldOfView = FOV;

        oldPos = playerTransform.position;
    }
}
