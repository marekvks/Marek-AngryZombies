using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WM : MonoBehaviour
{
    public int weaponSelected = 0;
    public int prevWeapon = 0;

    public PlayerCombat playerCombat;
    public UIManager uiManager;

    public List<CurrentGun> guns = new List<CurrentGun>();
    public GameObject prevGunModel;

    [System.Serializable]
    public class CurrentGun
    {
        public string weaponName;
        public GameObject gunModel;
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
    }

    private void Start()
    {
        prevGunModel = guns[0].gunModel;
        ChangeGun(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && weaponSelected != 0)
        {
            ChangeGun(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2) && weaponSelected != 1)
        {
            ChangeGun(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3) && weaponSelected != 2)
        {
            ChangeGun(2);
        }
    }

    private void ChangeGun(int index)
    {
        weaponSelected = index;
        prevGunModel.SetActive(false);
        CurrentGun currentGun = guns[index];
        currentGun.gunModel.SetActive(true);
        prevGunModel = currentGun.gunModel;
        playerCombat.muzzleFlash = currentGun.muzzleFlash;
        playerCombat.weaponSelected = weaponSelected;
        playerCombat.gunShotSound = currentGun.gunShotSound;
        playerCombat.reloadSound = currentGun.reloadSound;
        playerCombat.damage = currentGun.damage;
        playerCombat.range = currentGun.range;
        playerCombat.cadence = currentGun.cadence;
        playerCombat.ammunition = currentGun.ammunition;
        playerCombat.magSize = currentGun.magSize;
        playerCombat.currentRoundsInMag = currentGun.currentRoundsInMag;
        playerCombat.reloadTime = currentGun.reloadTime;
        playerCombat.ammunitionType = currentGun.ammunitionType;
        uiManager.ChangeText(uiManager.currentWeapon, currentGun.weaponName);
    }
}
