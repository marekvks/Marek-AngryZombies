using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform gunEndpoint;
    public AudioSource gunAudioSource;
    public AudioClip m16GunSound;

    private float gunRange = 1000f;
    private float currentGunShootInterval = 0.15f;
    private float shootInterval = 0f;
    private float damage = 10f;
    public LayerMask layer;
    public ParticleSystem muzzleFlash;
    RaycastHit hit;
    private bool isPlayed = false;

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);  // Draw Ray because im stupid and i dont remember wich direction should it point :)

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval)
        {
            shootInterval = Time.time + currentGunShootInterval;
            Shoot();    // bang bang
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gunEndpoint.position, gunRange);
    }
}
