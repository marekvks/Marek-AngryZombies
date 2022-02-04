﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    PlayerCombat playerCombat;
    public string currentGun;
    private GameObject currentGameObject;


    [Header("M16")]
    public GameObject M16Object;
    public ParticleSystem m16MuzzleFlash;
    public AudioClip m16GunSound;
    private string m16AmmunitionType = "5.56mm";

    [Header("UMP-45")]
    public GameObject UMPObject;
    public ParticleSystem umpMuzzleFlash;
    public AudioClip umpGunSound;
    private string umpAmmunitionType = ".45";

    [Header("Shotgun")]
    public GameObject ShotgunObject;
    public ParticleSystem shotgunMuzzleFlash;
    public AudioClip shotgunGunSound;
    private string shotgunAmmunitionType = "shotgun bolts";

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        currentGameObject = M16Object;
        M16();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Alpha1) && currentGun != "M16")
        {
            M16();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UMP();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Shotgun();
        }
    }

    private void M16()
    {
        currentGun = "M16";
        currentGameObject.SetActive(false);
        currentGameObject = M16Object;
        M16Object.SetActive(true);
        playerCombat.cadence = 0.2f;
        playerCombat.reloadTime = 0.3f;
        playerCombat.damage = 10f;
        playerCombat.gunSound = m16GunSound;
        playerCombat.range = 10f;
        playerCombat.ammunitionAmmount = 200f;
        playerCombat.magSize = 30f;
        playerCombat.ammunitionType = m16AmmunitionType;
    }
    
    private void UMP()
    {
        currentGun = "UMP";
        currentGameObject.SetActive(false);
        currentGameObject = UMPObject;
        UMPObject.SetActive(true);
        playerCombat.cadence = 0.1f;
        playerCombat.reloadTime = 0.3f;
        playerCombat.damage = 5f;
        playerCombat.gunSound = umpGunSound;
        playerCombat.range = 6f;
        playerCombat.ammunitionAmmount = 200f;
        playerCombat.magSize = 20f;
        playerCombat.ammunitionType = umpAmmunitionType;
    }
    
    private void Shotgun()
    {
        currentGun = "Shotgun";
        ShotgunObject.SetActive(true);
        currentGameObject.SetActive(false);
        currentGameObject = ShotgunObject;
        playerCombat.cadence = 1f;
        playerCombat.reloadTime = 0.5f;
        playerCombat.damage = 50f;
        playerCombat.gunSound = shotgunGunSound;
        playerCombat.range = 4f;
        playerCombat.ammunitionAmmount = 20f;
        playerCombat.magSize = 6f;
        playerCombat.ammunitionType = shotgunAmmunitionType;
    }
}
