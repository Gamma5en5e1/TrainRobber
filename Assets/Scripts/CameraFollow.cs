using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform player;
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    private Vector3 velocity = Vector3.zero;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
    }

}
