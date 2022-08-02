using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;
    float timeSinceLastShot;
    [SerializeField] Camera fpsCam;


    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, gunData.maxDistance))
                {
                    IDamageable target = hitInfo.transform.GetComponent<IDamageable>();
                    
                    target?.TakeDamage(gunData.damage);
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot(hitInfo);
            }
        }
    }

    void OnGunShot(RaycastHit hit)
    {
        muzzleFlash.Play();

        Vector3 hitNormal = hit.normal;
        hitNormal.x += 0.00001f;

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hitNormal));
        Destroy(impact, 1f);
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

}
