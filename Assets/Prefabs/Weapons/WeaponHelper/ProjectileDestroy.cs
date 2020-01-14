using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    public GameObject spawnOnDestruct = null;
    public float timeTillDestroy = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject.transform.root.gameObject, timeTillDestroy);
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy for now, but in the future we may damage instead of destroying
        if (spawnOnDestruct)
            Instantiate(spawnOnDestruct);
        Destroy(other.transform.root.gameObject);
        Destroy(gameObject.transform.root.gameObject);
    }
}
