using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform gunEndpoint;
    public AudioSource gunAudioSource;
    public AudioClip m16GunSound;
    private float shootInterval;
    public LayerMask layer;
    public ParticleSystem muzzleFlash;
    RaycastHit hit;
    private bool isPlayed = false;

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
     }

    private void Shoot()
    {
        gunAudioSource.PlayOneShot(m16GunSound);
        if (!isPlayed)
        {
            muzzleFlash.Play();
            isPlayed = true;
        }
        if (Physics.Raycast(gunEndpoint.position, transform.forward, out hit, Mathf.Infinity, layer))
        {
            Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.SetActive(false);
        }
        isPlayed = false;
    }
}
