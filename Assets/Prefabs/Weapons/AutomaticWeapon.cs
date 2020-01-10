using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : MonoBehaviour
{
    public GameObject projectile = null;
    public float fireRate = 10f;

    bool canShoot = true;
    
    public void Shoot()
    {
        if(canShoot)
        {
            // TEMP root
            Instantiate(projectile, transform.root.position,transform.root.rotation);
            canShoot = false;
            StartCoroutine("MakeShootable");
        }
    } 

    private void MakeShootable()
    {
        canShoot = true;
    }

}
