using System.Collections;
using UnityEngine;
using UnityTemplateProjects;

namespace Boss.Behaviours
{
    public class SpinShootBehaviour : BaseBossBehaviour
    {
        public override bool CanExecute => characterData.Health < 50f;

        public override bool DoneExecuting => spinShoot == null;

        [SerializeField] private ShootScript shootScript;
        [SerializeField] private IDamageable.DamageData damageData;
        [SerializeField] private int bulletAmount = 10;
        [SerializeField] private float bulletDelay = 0.1f;

        private Coroutine spinShoot;

        public override void OnExecute()
        {
            spinShoot = StartCoroutine(SpinShoot());
        }

        IEnumerator SpinShoot()
        {
            var currentBulletAmount = 0;

            while (currentBulletAmount < bulletAmount)
            {
                currentBulletAmount++;
                transform.Rotate(Vector3.up,360f / bulletAmount);
                shootScript.Shoot(new BulletData()
                {
                    direction = transform.forward,
                    speed = 15f,
                    lifetime = 10f,
                    grouping = IDamageable.Grouping.Enemy,
                    damageData = this.damageData
                });
                yield return new WaitForSeconds(bulletDelay);
            }
            
            spinShoot = null;
        }
    }
}