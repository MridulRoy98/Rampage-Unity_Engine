using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;
    [SerializeField] private Vector3 cameraOffset;

    public event EventHandler OnTriggerPoint;

    void Update()
    {
        transform.position = new Vector3(cameraOffset.x, cameraOffset.y, transform.position.z - (offsetAmount * offsetSpeed * Time.deltaTime));
    }
}
