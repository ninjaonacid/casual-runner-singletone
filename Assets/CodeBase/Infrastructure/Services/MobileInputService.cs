using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services
{
    public class MobileInputService : InputService
    {
        private readonly SwipeManager _swipeManager;
        private Touch _touch;
        public MobileInputService(SwipeManager swipeManager)
        {
            _swipeManager = swipeManager;
        }
        public override bool JumpButtonPressed() =>
                            _swipeManager.SwipeUp;
        
        public override bool SlideButtonPressed() =>
                            _swipeManager.SwipeDown;

        public override bool StartButtonPressed() =>
            Input.touchCount > 0;
        
            
        

        public override void Move(Transform transform, float speedModifier)
        {
            if (Input.touchCount > 0)
            {

                _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + _touch.deltaPosition.x *
                      speedModifier, transform.position.y, transform.position.z);

                }
            }
        }

    }
}