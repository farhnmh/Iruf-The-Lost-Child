using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootScript : MonoBehaviour
{
    [SerializeField] private BaseBullet bulletPrefab;
    [SerializeField] private int prePoolAmount = 10;
    [SerializeField] private Transform shootAnchor;

    private void Awake()
    {
        SimplePool.Preload(bulletPrefab.gameObject, prePoolAmount);
    }

    public void Shoot(BulletData data)
    {
        BaseBullet bullet = SimplePool.Spawn(bulletPrefab.gameObject, shootAnchor.position, Quaternion.identity)
            .GetComponent<BaseBullet>();
        
        bullet.Initialize(data);
    }
}
