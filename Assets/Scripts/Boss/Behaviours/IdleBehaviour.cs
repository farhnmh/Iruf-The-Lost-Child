using UnityEngine;

namespace Boss.Behaviours
{
    public class IdleBehaviour : BaseBossBehaviour
    {
        [SerializeField] private float idleTime = 5f;

        private float timer = 0f;

        public override bool DoneExecuting => timer >= idleTime;

        public override void OnExecute() => timer = 0f;

        public override void OnExecuteUpdate() => timer += Time.deltaTime;

        public override void OnInterrupt() => timer = idleTime;
    }
}