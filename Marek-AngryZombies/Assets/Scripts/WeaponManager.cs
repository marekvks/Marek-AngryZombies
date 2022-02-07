using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    /*PlayerCombat playerCombat;
    UIManager uiManager;

    public string currentGun;
    private GameObject currentGameObject;


    [Header("M16")]
    public GameObject M16Object;
    public ParticleSystem m16MuzzleFlash;
    public AudioClip m16GunSound;
    public float maxM16Ammo = 200f;
    public float currentM16AmmoInMag = 30f;
    private string m16AmmunitionType = "5.56mm";

    [Header("UMP-45")]
    public GameObject UMPObject;
    public ParticleSystem umpMuzzleFlash;
    public AudioClip umpGunSound;
    public float maxSMGAmmo = 200f;
    public float currentSMGAmmoInMag = 20f;
    private string umpAmmunitionType = ".45";

    [Header("Shotgun")]
    public GameObject ShotgunObject;
    public ParticleSystem shotgunMuzzleFlash;
    public AudioClip shotgunGunSound;
    public AudioClip shotgunReloadSound;
    public float maxShotgunBolts = 30f;
    public float currentSBInMag = 6f;
    private string shotgunAmmunitionType = "shotgun bolts";

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        uiManager = FindObjectOfType<UIManager>();
        currentGameObject = M16Object;
        M16();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Alpha1) && currentGun != "M16")
        {
            M16();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentGun != "UMP")
        {
            UMP();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentGun != "Shotgun")
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
        playerCombat.reloadTime = 2.2f;
        playerCombat.damage = 10f;
        playerCombat.gunSound = m16GunSound;
        playerCombat.range = 10f;
        playerCombat.ammunitionAmmount = maxM16Ammo;
        playerCombat.magSize = 30f;
        playerCombat.currentRoundsInMag = currentM16AmmoInMag;
        playerCombat.ammunitionType = m16AmmunitionType;
        uiManager.ChangeText(uiManager.currentWeapon, currentGun);
    }
    
    private void UMP()
    {
        currentGun = "UMP";
        currentGameObject.SetActive(false);
        currentGameObject = UMPObject;
        UMPObject.SetActive(true);
        playerCombat.cadence = 0.1f;
        playerCombat.reloadTime = 2.2f;
        playerCombat.damage = 5f;
        playerCombat.gunSound = umpGunSound;
        playerCombat.range = 6f;
        playerCombat.ammunitionAmmount = maxSMGAmmo;
        playerCombat.magSize = 20f;
        playerCombat.currentRoundsInMag = currentSMGAmmoInMag;
        playerCombat.ammunitionType = umpAmmunitionType;
        uiManager.ChangeText(uiManager.currentWeapon, currentGun);
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
        playerCombat.ammunitionAmmount = maxShotgunBolts;
        playerCombat.magSize = 6f;
        playerCombat.currentRoundsInMag = currentSBInMag;
        playerCombat.ammunitionType = shotgunAmmunitionType;
        playerCombat.reloadSound = shotgunReloadSound;
        uiManager.ChangeText(uiManager.currentWeapon, currentGun);
    }*/
}
