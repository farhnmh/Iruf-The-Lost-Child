using System;
using UnityEngine;

namespace Boss
{
    public abstract class BaseBossBehaviour : MonoBehaviour
    {
        public virtual bool CanExecute => true;

        public virtual bool CanInterrupt => true;

        public virtual bool DoneExecuting => true;

        protected CharacterData characterData;

        public virtual void OnInit(CharacterData data)
        {
            this.characterData = data;
        }

        public virtual void OnExecute(){}

        public virtual void OnExecuteUpdate(){}

        public virtual void OnInterrupt(){}
        
        public virtual void OnDoneExecuting(){}
    }
}