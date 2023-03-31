using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController cc;
    private Quaternion targetRotation;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float offsetAmount = 0.5f;
    [SerializeField] private int offsetSpeed = 3;


    private Vector3 moveDirection;
    private float rotationTimer = 0f;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();

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
        //Constantly moving player backwards to match the floor's speed
        Vector3 playerOffset = Vector3.forward * offsetSpeed * Time.deltaTime;
        //cc.Move(playerOffset);

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


        Move();
    }

    private void Move()
    {

    }
}
