using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShootScript : MonoBehaviour
{
    [SerializeField] private BaseBullet bulletPrefab;
    [SerializeField] private int prePoolAmount = 10;
    [SerializeField] private Transform shootAnchor;
    [SerializeField] private AudioClip shootSfx;

    private void Awake()
    {
        SimplePool.Preload(bulletPrefab.gameObject, prePoolAmount);
    }

    public void Shoot(BulletData data)
    {
        BaseBullet bullet = SimplePool.Spawn(bulletPrefab.gameObject, shootAnchor.position, Quaternion.identity)
            .GetComponent<BaseBullet>();
        
        bullet.Initialize(data);
        SoundManager.Instance.PlaySFX(shootSfx, Random.Range(0.8f, 1.2f), Random.Range(0.7f, 1.3f));
    }
}
