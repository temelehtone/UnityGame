using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Recoil recoilScript;



    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;
    [SerializeField] Camera fpsCam;
    [Header("Settings")]
    [SerializeField] float aimSpeed;
    float timeSinceLastShot;

    GameObject weaponHolder;
    Vector3 aimPosition;
    Vector3 weaponHolderPosition;
    GameObject reticle;

    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        weaponHolder = GameObject.Find("Weapon Holder");
        aimPosition = new Vector3(0, -0.08f, 0);
        weaponHolderPosition = weaponHolder.transform.localPosition;

        reticle = GameObject.Find("Reticle");
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

        recoilScript.RecoilFire();

        Vector3 hitNormal = hit.normal;
        hitNormal.x += 0.00001f;

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hitNormal));
        Destroy(impact, 5f);
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

    void StartAiming() {
        
        weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, aimPosition, aimSpeed * Time.deltaTime);
        reticle.SetActive(false);
    }
    void StopAiming() {
        weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, weaponHolderPosition, aimSpeed * Time.deltaTime);
        reticle.SetActive(true);
    }

    public bool aiming
    {
        get
        {
            if (
                Input.GetMouseButton(1))
            {
                StartAiming();
                return true;
            }
            else
            {
                StopAiming();
                return false;
            }
        }
    }

}
