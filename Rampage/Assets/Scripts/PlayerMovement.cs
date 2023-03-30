using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController cc;

    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private float rotatingSpeed = 2f;

    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        Debug.Log("Animator Found");
    }

    private bool isRunning()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            return true;
        }
        return false;
    }

    private bool isNotRunning()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetAmount * (offsetSpeed * Time.deltaTime));

        if (isRunning())
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        if (isNotRunning())
        {
            playerAnimator.SetBool("isNotRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isNotRunning", false);
        }
    }

    private void Move()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotatingSpeed, 0);
        Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
        float curSpeed = runningSpeed * Input.GetAxis("Vertical");
        if (isRunning())
        {
            cc.SimpleMove(moveDirection * curSpeed);
        }
    }
}
