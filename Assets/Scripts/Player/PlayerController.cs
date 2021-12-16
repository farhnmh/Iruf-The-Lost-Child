using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private ShootScript shootScript;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private IDamageable.DamageData damageData;
    [SerializeField] private Animator animator;
    [SerializeField] private HurtEffect hurtEffect;
    [SerializeField] private AudioClip hurtAudio;

    [SerializeField] private GameObject playerMesh;
    [SerializeField] private bool isDashing;
    private bool isMoving;

    [SerializeField] private float dashTimer;

    [Header("Feedbacks")] 
    [SerializeField] private MMFeedbacks shootFeedback;

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

    private void OnEnable()
    {
        characterData.OnHealthZero += OnPlayerDeath;
    }

    private void OnDisable()
    {
        characterData.OnHealthZero -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        
    }

    private void InputUpdate()
    {
        if (GameManager.Instance.IsGameOver)
        {
            moveDir = Vector3.zero;
            return;
        }
        
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        isMoving = moveDir.magnitude > 0;

        animator.SetBool("isWalking", isMoving);

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
                    damage = 2f
                }
            });
            
            shootFeedback?.PlayFeedbacks();
        }
    }

    private void MoveUpdate()
    {
        transform.position += moveDir.normalized * ((isDashing ? dashSpeed : moveSpeed) * Time.deltaTime);

        if (moveDir.magnitude > 0)
            playerMesh.transform.forward = moveDir;
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
        if (GameManager.Instance.IsGameOver) return;

        Debug.Log($"Player Damaged by {damageData.damage}");
        characterData.Health -= damageData.damage;
        hurtEffect.StartEffect();
        SoundManager.Instance.PlaySFX(hurtAudio, Random.Range(0.8f, 1.2f), Random.Range(0.7f, 1.3f));
    }
}
