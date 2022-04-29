using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services
{
    public abstract class InputService
    {

        public abstract bool JumpButtonPressed();
        public abstract void Move(Transform transform, float speedModifier);

        public abstract bool SlideButtonPressed();

        public abstract bool StartButtonPressed();
        
            
        
    }

    
}