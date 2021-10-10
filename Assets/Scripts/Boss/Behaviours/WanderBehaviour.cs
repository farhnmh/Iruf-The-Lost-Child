using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boss.Behaviours
{
    public class WanderBehaviour : BaseBossBehaviour
    {
        [SerializeField] private float speed;

        private bool isWalking = false;
        private Vector2 randomDirection;
        private Vector3 walkDirection;
        private Vector3 destination;

        public override bool DoneExecuting => transform.position == destination;

        public override void OnExecute()
        {
            isWalking = true;
            var direction = Random.insideUnitCircle;
            walkDirection.x = direction.x;
            walkDirection.z = direction.y;
        }

        public override void OnInterrupt()
        {
            isWalking = false;
        }

        private void Update()
        {
            if (isWalking)
            {
                transform.position += walkDirection * (speed * Time.deltaTime);
            }
        }
    }
}