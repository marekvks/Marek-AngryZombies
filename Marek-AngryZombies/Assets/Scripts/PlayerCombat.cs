using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform gunEndpoint;
    public AudioSource gunAudioSource;
    public AudioClip m16GunSound;
    private float currentGunShootInterval = 0.15f;
    private float shootInterval = 0f;
    private float damage = 10f;
    public LayerMask layer;
    public ParticleSystem muzzleFlash;
    RaycastHit hit;
    private bool isPlayed = false;

    /*       |
     *      /|\
     *     / | \
     *    /  |  \
     *       |
     *       |
     *       |
     * 
     * well-arranged code here ------> jk i will fix this but ill do it tomorrow
     * 
     * 
     */

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);  // Draw Ray because im stupid and i dont remember wich direction should it point :)

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootInterval)
        {
            shootInterval = Time.time + currentGunShootInterval;    // time wtf
            Shoot();    // bang bang
            muzzleFlash.Stop();
        }
     }

    private void Shoot()
    {
        gunAudioSource.PlayOneShot(m16GunSound);    // boom!
        if (!isPlayed)
        {
            muzzleFlash.Play();     // Play muzzleFlash particle but ... it doesnt work yet :D
            isPlayed = true;
        }
        if (Physics.Raycast(gunEndpoint.position, transform.forward, out hit, Mathf.Infinity, layer))   // Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray Ray
        {
            Debug.Log(hit.collider.gameObject.name);                            // Why debug.log? i dont know
            hit.collider.GetComponent<EnemyHealth>().zombieHealth -= damage;    // Deals damage to the player lol
        }


        isPlayed = false;
    }
}
