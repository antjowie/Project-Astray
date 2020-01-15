using UnityEngine;

public class ForwardMove : MonoBehaviour
{
    public float force = 50f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
