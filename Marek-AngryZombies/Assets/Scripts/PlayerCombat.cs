using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Objects/Components")]
    public Transform gunEndpoint;
    public LayerMask layer;
    public ParticleSystem m16MuzzleFlash;
    public ParticleSystem umpMuzzleFlash;
    public ParticleSystem shotgunMuzzleFlash;

    private WeaponManager weaponManager;
    private UIManager uiManager;

    public GameObject impact;
    public GameObject blood;

    [Header("Audio")]
    private SoundManager soundManager;
    public AudioClip m16Reload;

    /*[Header("Variables")]
    private float gunRange = 1000f;
    private float currentGunShootInterval = 0.1f;
    private float damage = 10f;
    private float shootInterval = 0f;*/
    private float muzzleFlashLifetime = 0.1f;
    private float stopMuzzleFlashTime;
    RaycastHit hit;

    [Header("Weapon Statistics")]
    public AudioClip gunSound;
    public AudioClip reloadSound;
    public float cadence;
    public float shootInterval;
    public float reloadTime;
    public float damage;
    public float range;
    public float ammunitionAmmount;
    public float magSize;
    public float currentRoundsInMag;
    public string ammunitionType;

    private float timeRequired;
    private bool canShoot = true;
    private bool canReload = false;
    private bool isReloading = false;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {

        uiManager.ChangeText(uiManager.ammoCarrying, ammunitionAmmount.ToString());
        uiManager.ChangeText(uiManager.ammoInMag, currentRoundsInMag.ToString());

        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval && canShoot)
        {
            shootInterval = Time.time + cadence;
            stopMuzzleFlashTime = Time.time + muzzleFlashLifetime;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentRoundsInMag < magSize && ammunitionAmmount > 0 && !isReloading)
        {
            canShoot = false;
            isReloading = true;
            timeRequired = Time.time + reloadTime;
            canReload = true;
            switch (weaponManager.currentGun)
            {
                case "Shotgun":
                    soundManager.GunSound(reloadSound);
                    break;
                case "M16":
                    soundManager.GunSound(m16Reload);
                    break;
                case "UMP":
                    soundManager.GunSound(m16Reload);
                    break;
            }
        }

        if (Time.time > timeRequired && canReload)
        {
            float ammoNeeded = 0;
            switch (weaponManager.currentGun)
            {
                case "M16":
                    ammoNeeded = magSize - currentRoundsInMag;
                    weaponManager.maxM16Ammo -= ammoNeeded;
                    weaponManager.currentM16AmmoInMag += ammoNeeded;
                    ammunitionAmmount = weaponManager.maxM16Ammo;
                    break;
                case "UMP":
                    ammoNeeded = magSize - currentRoundsInMag;
                    weaponManager.maxSMGAmmo -= ammoNeeded;
                    weaponManager.currentSMGAmmoInMag += ammoNeeded;
                    ammunitionAmmount = weaponManager.maxSMGAmmo;
                    break;
                case "Shotgun":
                    if (currentRoundsInMag <= 6)
                    {
                        ammoNeeded = 1;
                    }
                    weaponManager.maxShotgunBolts -= ammoNeeded;
                    weaponManager.currentSBInMag += ammoNeeded;
                    ammunitionAmmount = weaponManager.maxShotgunBolts;
                    break;
            }
            
            //  Change text
            /***************************************************************************************/
            currentRoundsInMag += ammoNeeded;
            canShoot = true;
            isReloading = false;
            canReload = false;
        }

        if (currentRoundsInMag <= 0 || ammunitionAmmount <= 0)
        {
            canShoot = false;
        } else if (currentRoundsInMag > 0 && !canReload)
        {
            canShoot = true;
        }

        if (Time.time > stopMuzzleFlashTime)
        {
            m16MuzzleFlash.Stop();
            umpMuzzleFlash.Stop();
            shotgunMuzzleFlash.Stop();
        }
     }

    private void Shoot()
    {
        currentRoundsInMag -= 1;
        switch (weaponManager.currentGun)
        {
            case "M16":
                weaponManager.currentM16AmmoInMag -= 1;
                break;
            case "UMP":
                weaponManager.currentSMGAmmoInMag -= 1;
                break;
            case "Shotgun":
                weaponManager.currentSBInMag -= 1;
                break;
        }

        uiManager.ChangeText(uiManager.ammoInMag, currentRoundsInMag.ToString());
        Debug.Log(currentRoundsInMag.ToString());

        switch (weaponManager.currentGun)
        {
            case "M16":
                m16MuzzleFlash.Play();
                break;
            case "UMP":
                umpMuzzleFlash.Play();
                break;
            case "Shotgun":
                shotgunMuzzleFlash.Play();
                break;
        }

        soundManager.GunSound(gunSound);

        if (Physics.Raycast(gunEndpoint.position, transform.forward, out hit, range))
        {

            Vector3 hitPosition = hit.point;
            Vector2 angle = transform.position - hitPosition;

            if (hit.collider.name.Contains("Zombie"))
            {
                GameObject impactParticle = Instantiate(blood);

                impactParticle.transform.position = hit.point;

                EnemyHealth zombieHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();

                if (zombieHealth != null)
                {
                    zombieHealth.TakeDamage(damage);
                }
            } else
            {
                Instantiate(impact, hitPosition, Quaternion.LookRotation(angle));
            }
        }
    }
}