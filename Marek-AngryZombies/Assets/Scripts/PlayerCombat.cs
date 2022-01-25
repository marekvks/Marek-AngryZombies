using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Objects/Components")]
    public Transform gunEndpoint;
    public AudioSource gunAudioSource;
    public LayerMask layer;
    public ParticleSystem muzzleFlash;

    [Header("Audio")]
    public AudioClip m16GunSound;

    [Header("Variables")]
    private float gunRange = 1000f;
    private float currentGunShootInterval = 0.1f;
    private float damage = 10f;
    private float shootInterval = 0f;
    private float saveInterval;
    private float muzzleFlashLifetime = 0.005f;
    RaycastHit hit;

    private void Start()
    {
        var main = muzzleFlash.main;
        main.startLifetime = muzzleFlashLifetime;
    }

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval)
        {
            shootInterval = Time.time + currentGunShootInterval;
            saveInterval = shootInterval;
            Shoot();
        }

        if (Time.time > saveInterval + muzzleFlashLifetime)
        {
            muzzleFlash.Stop();
        }
     }

    private void Shoot()
    {
        muzzleFlash.Play();
        gunAudioSource.PlayOneShot(m16GunSound);    // boom!
        if (Physics.Raycast(gunEndpoint.position, transform.forward, out hit, gunRange, layer))
        {
            EnemyHealth zombieHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();

            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage();
            }
        }
    }
}
