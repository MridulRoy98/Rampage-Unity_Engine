using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Movement Stats")]
    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;
    [SerializeField] private Vector3 cameraOffset;

    public event EventHandler <OnTriggerPointEventArgs> OnTriggerPoint;
    private float initialTriggerPosition;
    private int difference;
    private int multiplier;
    private bool hasTriggered = false;

    public class OnTriggerPointEventArgs : EventArgs
    {
        public int triggerCount;
    }

    private void Start()
    {
        initialTriggerPosition = 144;
        difference = 48;
        multiplier = 1;
    }
    private void Update()
    {
        //Constantly move camera forward
        transform.position = new Vector3(cameraOffset.x, cameraOffset.y, transform.position.z - (offsetAmount * offsetSpeed * Time.deltaTime));
        CameraTrigger();
    }

    //Publish event when camera is far enough to destroy previously spawned platform
    private void CameraTrigger()
    {
        if (!hasTriggered && transform.position.z <= initialTriggerPosition - difference * multiplier)
        {
            OnTriggerPoint?.Invoke(this, new OnTriggerPointEventArgs { triggerCount = multiplier });
            multiplier++;
            hasTriggered = true;
        }
        if (hasTriggered && transform.position.z <= initialTriggerPosition - difference * multiplier+5)
        {
            hasTriggered = false;
        }
    }
}
//