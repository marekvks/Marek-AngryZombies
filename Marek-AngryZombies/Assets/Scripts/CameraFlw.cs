using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlw : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 6, -4);

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
