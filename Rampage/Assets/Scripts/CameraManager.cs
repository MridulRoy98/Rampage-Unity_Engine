using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Movement Stats")]
    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;
    [SerializeField] private Vector3 cameraOffset;

    private float initialTriggerPosition = 144;
    private int difference = 48;
    private int multiplier = 1;
    private float targetCameraZPosition;

    public event EventHandler OnTriggerPoint;

    private void Update()
    {
        //Constantly move camera forward
        transform.position = new Vector3(cameraOffset.x, cameraOffset.y, transform.position.z - (offsetAmount * offsetSpeed * Time.deltaTime));
        CameraTrigger();
    }

    //Publish event when camera is far enough to destroy previously spawned platform
    private void CameraTrigger()
    {
        if (transform.position.z <= initialTriggerPosition - difference * multiplier)
        {
            OnTriggerPoint?.Invoke(this, EventArgs.Empty);
            multiplier++;
        }
    }

}
