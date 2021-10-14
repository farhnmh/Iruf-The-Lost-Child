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

        private Coroutine behaviourCoroutine;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            behaviourCoroutine = StartCoroutine(StartBehaviour());
        }

        private void Awake()
        {
            OnAwake();
            
            foreach (var behaviour in behaviours)
                behaviour.OnInit(bossData);

            bossData.OnHealthZero += OnBossDeath;
        }

        protected virtual void OnAwake(){}

        protected virtual IEnumerator StartBehaviour()
        {
            while (true)
            {
                foreach (var behaviour in behaviours)
                {
                    if(!behaviour.CanExecute) continue;
                
                    behaviour.OnExecute();
                
                    Debug.Log($"Executing {behaviour.GetType()}");

                    while (!behaviour.DoneExecuting)
                    {
                        behaviour.OnExecuteUpdate();
                        yield return null;
                    }
                
                    behaviour.OnDoneExecuting();
                    Debug.Log($"Done Executing {behaviour.GetType()}");
                }
            }
        }

        protected virtual void StartInterrupt()
        {
            if (behaviourCoroutine == null) return;
            
            StopCoroutine(behaviourCoroutine);
            foreach (var behaviour in behaviours)
            {
                if(behaviour.CanInterrupt) behaviour.OnInterrupt();
            }
        }

        public IDamageable.Grouping Group => IDamageable.Grouping.Enemy;
        public void Damage(IDamageable.DamageData damageData)
        {
            bossData.Health -= damageData.damage;
            Debug.Log($"<color=red>Boss Damaged for {damageData.damage}. Health is now {bossData.health}</color>");
        }

        protected virtual void OnBossDeath()
        {
            
        }
    }
}
