using UnityEngine;

namespace Boss.Behaviours
{
    public class SpinShootBehaviour : BaseBossBehaviour
    {
        public override bool CanExecute => bossData.health > 50f;
        [SerializeField] private ShootScript shootScript;

    }
}