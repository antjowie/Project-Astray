using UnityEngine;

public class TraceWeapon : WeaponInterface
{
    [SerializeField] private GameObject traceProjectile;
    [SerializeField] private float chargeTime = 0f;

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
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
            var scale = traceProjectile.transform.localScale;
            scale.z = 10;//hit.distance;
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
