using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services
{
    public class MobileInputService : InputService
    {
        
        private Touch _touch;

        public override bool JumpButtonPressed() =>
                            SwipeControls.Instance.SwipeUp;

        public override bool SlideButtonPressed() =>
                            SwipeControls.Instance.SwipeDown;

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