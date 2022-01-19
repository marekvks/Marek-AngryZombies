using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform gunEndpoint;
    public LayerMask layer;
    [SerializeField]
    private float damage;
    // private float reloadTime;

    RaycastHit hit;

    private void Update()
    {
        Debug.DrawRay(gunEndpoint.position, transform.forward, Color.red);

        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(gunEndpoint.position, transform.forward, out hit, Mathf.Infinity, layer))
        {
            hit.collider.gameObject.SetActive(false);
        }
    }
}
