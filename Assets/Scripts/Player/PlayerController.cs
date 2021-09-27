using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private Vector3 moveDir;

    //Todo: Use state enum instead
    [SerializeField] private bool isDashing;
    private bool isMoving;

    [SerializeField] private float dashTimer;

    private Camera cam;
    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        InputUpdate();
        LookAtUpdate();
        DashUpdate();
        MoveUpdate();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            DashMovement();
        }
        
    }

    private void InputUpdate()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        isMoving = moveDir.magnitude > 0;
    }

    private void MoveUpdate()
    {
        transform.position += moveDir.normalized * (moveSpeed * Time.deltaTime);
    }

    private void LookAtUpdate()
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        rotationAnchor.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void DashUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            moveSpeed = moveSpeed * 10f;
            dashTimer = 0.15f;
        }
    }

    private void DashMovement()
    {
        if(dashTimer > 0)
        {
            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            moveSpeed = moveSpeed / 10f;
            isDashing = false;
            
        }

    }
}
