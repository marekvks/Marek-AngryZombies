using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string gunModel;
    public WM weaponManager;

    public float addM16Ammo = 30f;
    public float addSmgAmmo = 30f;
    public float addShotgunBolts = 10f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            weaponManager = col.GetComponentInChildren<WM>();
            switch (gunModel)
            {
                case "M16":
                    weaponManager.guns[0].ammunition += addM16Ammo;
                    break;
                case "SMG":
                    weaponManager.guns[1].ammunition += addSmgAmmo;
                    break;
                case "Shotgun":
                    weaponManager.guns[2].ammunition += addShotgunBolts;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
