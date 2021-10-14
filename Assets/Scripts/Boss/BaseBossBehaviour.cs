using System;
using UnityEngine;

namespace Boss
{
    public abstract class BaseBossBehaviour : MonoBehaviour
    {
        public virtual bool CanExecute => true;

        public virtual bool CanInterrupt => true;

        public virtual bool DoneExecuting => true;

        protected BossData bossData;

        public virtual void OnInit(BossData data)
        {
            this.bossData = data;
        }

        public virtual void OnExecute(){}

        public virtual void OnExecuteUpdate(){}

        public virtual void OnInterrupt(){}
        
        public virtual void OnDoneExecuting(){}
    }
}