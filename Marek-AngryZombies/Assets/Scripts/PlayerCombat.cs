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

                                                                        private WM wM;
    
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
    public int weaponSelected;
    public ParticleSystem muzzleFlash;
    public AudioClip gunShotSound;
    public AudioClip reloadSound;
    public float damage;
    public float range;
    public float cadence;
    public float ammunition;
    public float magSize;
    public float currentRoundsInMag;
    public float reloadTime;
    public string ammunitionType;

    public float shootInterval;


    /*public AudioClip gunSound;
    public AudioClip reloadSound;
    public float cadence;
    public float reloadTime;
    public float damage;
    public float range;
    public float ammunitionAmmount;
    public float magSize;
    public float currentRoundsInMag;
    public string ammunitionType;*/

    private float timeRequired;
    private bool canShoot = true;
    private bool canReload = false;
    private bool isReloading = false;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        wM = FindObjectOfType<WM>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {

        uiManager.ChangeText(uiManager.ammoCarrying, ammunition.ToString());
        uiManager.ChangeText(uiManager.ammoInMag, currentRoundsInMag.ToString());

        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval && canShoot)
        {
            shootInterval = Time.time + cadence;
            stopMuzzleFlashTime = Time.time + muzzleFlashLifetime;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentRoundsInMag < magSize && ammunition > 0 && !isReloading)
        {
            canShoot = false;
            isReloading = true;
            timeRequired = Time.time + reloadTime;
            canReload = true;

            soundManager.GunSound(wM.guns[weaponSelected].reloadSound);
        }

        if (Time.time > timeRequired && canReload)
        {
            float ammoNeeded = 0;
            if (weaponSelected == 2 && currentRoundsInMag <= 6)
            {
                ammoNeeded = 1;
            }
            else
            { 
                ammoNeeded = magSize - currentRoundsInMag;
            }

            wM.guns[weaponSelected].ammunition -= ammoNeeded;
            wM.guns[weaponSelected].currentRoundsInMag += ammoNeeded;
            ammunition = wM.guns[weaponSelected].ammunition;

            currentRoundsInMag += ammoNeeded;
            canShoot = true;
            isReloading = false;
            canReload = false;
        }

        if (currentRoundsInMag <= 0 || ammunition <= 0)
        {
            canShoot = false;
        } else if (currentRoundsInMag > 0 && !canReload)
        {
            canShoot = true;
        }

        if (Time.time > stopMuzzleFlashTime)
        {
            muzzleFlash.Stop();
        }
     }

    private void Shoot()
    {
        currentRoundsInMag -= 1;
        wM.guns[weaponSelected].currentRoundsInMag -= 1;

        uiManager.ChangeText(uiManager.ammoInMag, currentRoundsInMag.ToString());

        muzzleFlash.Play();

        soundManager.GunSound(gunShotSound);

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