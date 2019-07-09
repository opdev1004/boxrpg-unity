﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; //turn speed in radians per second
    public float movementSpeed = 8.0f;
    public float jumpForce = 10.0f;
    public List<KeyCode> jumpKey = new List<KeyCode>{ KeyCode.Space, KeyCode.JoystickButton0 };

    Rigidbody m_Rigidbody;
    BoxCollider m_BoxCollider;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    bool isJumping;
    float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BoxCollider = GetComponent<BoxCollider>();
        jumpTime = 0f;
    }

    // Instead of being called before every rendered frame like Update(), FixedUpdate is called before the physics system solves any collisions and other interactions that have happened.  By default it is called exactly 50 times every second.
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        if (isWalking)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);

            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * movementSpeed * Time.deltaTime);

            if (Physics.Raycast(m_Rigidbody.position, Vector3.down, m_BoxCollider.size.y / 1.95f))
            {
                m_Rigidbody.MoveRotation(m_Rotation);
            }
        }

        Jump();
    }

    //runs every frame
    void Update()
    {
        
        //check if the player is standing on a solid object
        if (Physics.Raycast(m_Rigidbody.position, Vector3.down, m_BoxCollider.size.y / 1.95f))
        {
            foreach (KeyCode key in jumpKey)
            {
                if (Input.GetKeyDown(key))
                {
                    isJumping = true;
                    jumpTime = 0.5f;
                }
            }
        }
    }

    void Jump()
    {
        if (isJumping)
        {
            if (jumpTime > 0)
            {
                Vector3 jumpvector = new Vector3(0, jumpForce, 0);
                m_Rigidbody.MovePosition(m_Rigidbody.position + jumpvector * movementSpeed * Time.deltaTime);
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }
}