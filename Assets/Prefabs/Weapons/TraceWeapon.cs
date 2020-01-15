using UnityEngine;

public class TraceWeapon : WeaponInterface
{
    [SerializeField] private GameObject traceProjectile;
    [SerializeField] private float chargeTime = 0f;
    [SerializeField] private float maxDistance = 10f;

    public void Start()
    {
        traceProjectile = Instantiate(traceProjectile, transform);
        traceProjectile.SetActive(false);
    }

    public override void OnPress()
    {
        Invoke("OnChargeDone", chargeTime);
    }

    public override void OnHold()
    {
        if (traceProjectile.activeInHierarchy)
        {
            int mask = ~LayerMask.NameToLayer("Player"); // If we add enemies this has to be changed
            var scale = traceProjectile.transform.localScale;
            bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, mask, QueryTriggerInteraction.Collide);
            
            // Set correct scale
            if (isHit) { scale.z = hit.distance * 0.5f; }
            else       { scale.z = maxDistance * 0.5f; }

            traceProjectile.transform.localScale = scale;
        }
    }

    public override void OnRelease()
    {
        CancelInvoke();
        traceProjectile.SetActive(false);
    }

    private void OnChargeDone()
    {
        traceProjectile.SetActive(true);
    }
}
