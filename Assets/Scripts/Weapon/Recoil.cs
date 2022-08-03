using UnityEngine;

public class Recoil : MonoBehaviour
{

    [Header("Scripts")]
    [SerializeField] Weapon weaponScript;
      [SerializeField] Reticle reticleScript;

    bool isAiming;
    bool isMoving;

    // Rotations
    Vector3 currentRotation;
    Vector3 targetRotation;

    [Header("Hipfire Settings")]
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;

    [Header("ADS Settings")]
    [SerializeField] float aimRecoilX;
    [SerializeField] float aimRecoilY;
    [SerializeField] float aimRecoilZ;

    [Header("Settings")]
    [SerializeField] float snappiness;
    [SerializeField] float returnSpeed;
    [SerializeField] float aimMoveMultiplier;
    [SerializeField] float hipfireMoveMultiplier;


    void Update()
    {

        isAiming = weaponScript.aiming;
        isMoving = reticleScript.isMoving;
        

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if (isAiming)
        {
            Vector3 recoil = new Vector3(aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
            targetRotation += isMoving ? recoil * aimMoveMultiplier : recoil;
        }
        else
        {
            Vector3 recoil = new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
            targetRotation += isMoving ? recoil * hipfireMoveMultiplier : recoil;
        }
    }

}
