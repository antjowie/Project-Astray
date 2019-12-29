using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * speed);
        rb.AddForce(transform.right * speed, ForceMode.VelocityChange);
        //rb.velocity = transform.forward * speed;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if(other.gameObject.GetComponentInParent<Rotator>())
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
