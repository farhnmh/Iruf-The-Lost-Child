using System;
using DG.Tweening;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

namespace Boss.Behaviours
{
    public class ChasePlayerBehaviour : BaseBossBehaviour
    {
        [SerializeField] private float moveTime = 2f;
        [SerializeField] private int chaseAmount = 3;

        public override bool DoneExecuting => doneChasing;

        private bool doneChasing = false;
        private int currentChaseAmount = 0;

        public override void OnExecute()
        {
            doneChasing = false;
            currentChaseAmount = 0;
            
            Chase();
        }

        private void Chase()
        {
            if (currentChaseAmount >= chaseAmount)
            {
                doneChasing = true;
                return;
            }

            var playerTransform = GameManager.Instance.Player.transform;
            var destination = playerTransform.position;

            transform.DOLookAt(destination, 0.1f).SetEase(Ease.OutQuart).OnComplete(delegate
            {
                transform.DOMove(destination, moveTime).SetEase(Ease.OutQuint).OnComplete(Chase);
            });
            currentChaseAmount++;
        }
    }
}