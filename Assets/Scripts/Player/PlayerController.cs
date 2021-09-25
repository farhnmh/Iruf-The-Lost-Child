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
    private bool isDashing;
    private bool isMoving;

    private Camera cam;
    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        InputUpdate();
        LookAtUpdate();
        if(isMoving && !isDashing) MoveUpdate();
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
        
    }
}
