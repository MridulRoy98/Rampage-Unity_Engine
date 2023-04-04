using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;
    [SerializeField] private Vector3 cameraOffset;
    void Update()
    {
        transform.position = new Vector3(cameraOffset.x, cameraOffset.y, transform.position.z - (offsetAmount * offsetSpeed * Time.deltaTime));
    }
}
