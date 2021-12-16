using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class DamageOnCollide : MonoBehaviour
    {
        [SerializeField] private IDamageable.DamageData damageData;
        [SerializeField] private IDamageable.Grouping targetGroup;
        private void OnCollisionEnter(Collision other)
        {
            Damage(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Damage(other.gameObject);
        }

        private void Damage(GameObject obj)
        {
            IDamageable[] damageables = obj.GetComponents<IDamageable>();

            if (damageables.Length <= 0) return;

            foreach (var damageable in damageables)
            {
                if (damageable.Group == targetGroup) damageable.Damage(damageData);
            } 
        }
    }
}