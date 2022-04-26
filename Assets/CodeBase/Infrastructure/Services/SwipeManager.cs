using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services
{
    public class SwipeManager : MonoBehaviour
    {

        private bool tap, swipeUp, swipeDown;
        private bool isDraging = false;
        private Vector2 startTouch, swipeDelta;

        public Vector2 SwipeDelta { get { return swipeDelta; } }

        public bool Tap { get { return tap; } }
        public bool SwipeUp { get { return swipeUp; } }
        public bool SwipeDown { get { return swipeDown; } }

        private void Reset()
        {
            startTouch = swipeDelta = Vector2.zero;
            isDraging = false;
        }


        private void Update()
        {
            tap = swipeUp = swipeDown = false;


            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDraging = true;
                    tap = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended ||
                          Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }


            // Расчет дистанции
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }
                else if (Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }
            }

            if (swipeDelta.magnitude > 125)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))

                {
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
                Reset();
            }
        }

    }
}