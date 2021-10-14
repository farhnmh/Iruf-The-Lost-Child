using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private ShootScript shootScript;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private IDamageable.DamageData damageData;

    [SerializeField] private bool isDashing;
    private bool isMoving;

    [SerializeField] private float dashTimer;

    private Camera cam;
    
    private void Awake()
    {
        cam = Camera.main;
        dashParticle.Stop();
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

        if (Input.GetButtonDown("Shoot"))
        {
            shootScript.Shoot(new BulletData()
            {
                direction = rotationAnchor.forward,
                speed = 30f,
                lifetime = 3f,
                grouping = Group,
                damageData = new IDamageable.DamageData()
                {
                    damage = 5f
                }
            });
        }
    }

    private void MoveUpdate()
    {
        transform.position += moveDir.normalized * ((isDashing ? dashSpeed : moveSpeed) * Time.deltaTime);
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
            dashTimer = 0.15f;
            dashParticle.Play();
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
            isDashing = false;
            dashParticle.Stop();
        }

    }

    public IDamageable.Grouping Group => IDamageable.Grouping.Player;
    public void Damage(IDamageable.DamageData damageData)
    {
        Debug.Log($"Player Damaged by {damageData.damage}");
    }
}
