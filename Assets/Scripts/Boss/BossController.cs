using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

namespace Boss
{
    public class BossController : MonoBehaviour, IDamageable
    {
        [SerializeField] private List<BaseBossBehaviour> behaviours;
        [SerializeField] private BossData bossData;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            foreach (var behaviour in behaviours)
                behaviour.OnInit(bossData);

        }

        protected virtual IEnumerator StartBehaviour()
        {
            foreach (var behaviour in behaviours)
            {
                if(!behaviour.CanExecute) continue;
                
                behaviour.OnExecute();

                while (!behaviour.DoneExecuting)
                {
                    behaviour.ExecuteUpdate();
                    yield return null;
                }
            }
        }

        protected virtual void StartInterrupt()
        {
            foreach (var behaviour in behaviours)
            {
                if(behaviour.CanInterrupt) behaviour.OnInterrupt();
            }
        }


        public IDamageable.Grouping Group => IDamageable.Grouping.Enemy;
        public void Damage(IDamageable.DamageData damageData)
        {
            StartInterrupt();
            bossData.health -= damageData.damage;
            if (bossData.health <= 0) OnBossDeath();
        }

        protected virtual void OnBossDeath()
        {
            
        }
    }
}
