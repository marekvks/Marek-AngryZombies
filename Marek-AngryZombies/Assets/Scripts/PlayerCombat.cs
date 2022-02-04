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

    public GameObject impact;
    public GameObject blood;

    [Header("Audio")]
    private SoundManager soundManager;
    public AudioClip m16GunSound;

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

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval && canShoot)
        {
            shootInterval = Time.time + cadence;
            stopMuzzleFlashTime = Time.time + muzzleFlashLifetime;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentRoundsInMag < magSize && ammunitionAmmount > 0)
        {
            switch (weaponManager.currentGun)
            {
                case "Shotgun":
                    soundManager.GunSound(reloadSound);
                    break;
            }
            canShoot = false;
            timeRequired = Time.time + reloadTime;
            canReload = true;
        }

        if (Time.time > timeRequired && canReload)
        {
            float ammoNeeded = magSize - currentRoundsInMag;
            switch (weaponManager.currentGun)
            {
                case "M16":
                    weaponManager.maxM16Ammo -= ammoNeeded;
                    break;
                case "UMP":
                    weaponManager.maxSMGAmmo -= ammoNeeded;
                    break;
                case "Shotgun":
                    weaponManager.maxShotgunBolts -= ammoNeeded;
                    break;
            }
            currentRoundsInMag += ammoNeeded;
            canShoot = true;
            canReload = false;
        }

        Debug.Log(ammunitionAmmount);

        if (currentRoundsInMag <= 0 || ammunitionAmmount <= 0)
        {
            canShoot = false;
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

            if (hit.collider.name.Contains("zombie"))
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