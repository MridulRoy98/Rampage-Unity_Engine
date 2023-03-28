using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;


    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();    
    }

    private bool isRunning()
    {
        if (Input.GetAxis("Vertical") != 0)
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
}
