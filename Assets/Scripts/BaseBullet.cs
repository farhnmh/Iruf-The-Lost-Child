using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BaseBullet : MonoBehaviour
{
    [SerializeField] private BulletData data;
    [SerializeField] private GameObject visual;
    [SerializeField] private bool isBillboard = true;

    private Rigidbody rb;
    private Camera cam;
    private bool hasInit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    public void Initialize(BulletData data)
    {
        this.data = data;

        rb.velocity = data.direction.normalized * data.speed;

        Invoke(nameof(Despawn), data.lifetime);
        hasInit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable[] damageables = other.GetComponents<IDamageable>();

        if (damageables.Length <= 0) return;

        foreach (var damageable in damageables)
        {
            if (damageable.Group == data.grouping) continue;
            damageable.Damage(data.damageData);
        }
    }

    private void Despawn()
    {
        SimplePool.Despawn(gameObject);
    }

    private void LateUpdate()
    {
        if (!isBillboard) return;
        visual.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
    }
}

[Serializable]
public struct BulletData
{
    public Vector3 direction;
    public float speed;
    public float lifetime;
    public IDamageable.DamageData damageData;
    public IDamageable.Grouping grouping;

}
