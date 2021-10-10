using System.Collections;
using UnityEngine;

namespace Boss.Behaviours
{
    public class TeleportBehaviour : BaseBossBehaviour
    {
        public override bool CanExecute => bossData.health < 20f;

        public override bool DoneExecuting => currentTeleportCount >= teleportCount;

        [SerializeField] private int teleportCount = 3;

        private int currentTeleportCount = 0;

        public override void OnExecute()
        {
            StartCoroutine(Teleport());
        }

        private IEnumerator Teleport()
        {
            //blablabla
            yield return null;
            currentTeleportCount++;
        }

        public override void OnInterrupt()
        {
            StopCoroutine(Teleport());
            
            //Play Failed Teleport ANimation
        }
    }
}