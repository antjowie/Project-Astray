using UnityEngine;

public class AutomaticWeapon : WeaponInterface
{
    public GameObject projectile = null;
    public float fireRate = 10f;

    bool canShoot = true;
    
    public override void OnHold()
    {
        if(canShoot)
        {
            // TEMP we should define some way to decide how projectiles move
            // For now we just use transform of the ship
            Instantiate(projectile, transform.root.position,transform.root.rotation);
            canShoot = false;

            Invoke("MakeShootable", 1f / fireRate);
        }
    } 

    private void MakeShootable()
    {
        canShoot = true;
    }

}
