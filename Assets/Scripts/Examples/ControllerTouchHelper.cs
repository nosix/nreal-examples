using NRKernal;
using UnityEngine;

namespace Examples
{
    public enum SwipeDirection
    {
        None,
        Left,
        Right,
        Down,
        Up
    }

    public class ControllerTouchHelper
    {
        private readonly float _swipeDistanceThreshold;

        public SwipeDirection Swipe { get; private set; }

        private float _startTime;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public ControllerTouchHelper(float swipeDistanceThreshold = 0.1f)
        {
            _swipeDistanceThreshold = swipeDistanceThreshold;
        }

        public void Update()
        {
            Swipe = SwipeDirection.None;

            if (NRInput.IsTouching())
            {
                if (Mathf.Abs(_startTime) < float.Epsilon)
                {
                    _startTime = Time.time;
                    _startPosition = NRInput.GetTouch();
                }

                _endPosition = NRInput.GetTouch();
                return;
            }

            if (Mathf.Abs(_startTime) < float.Epsilon) return;

            var direction = _endPosition - _startPosition;
            if (direction.magnitude >= _swipeDistanceThreshold)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    Swipe = direction.x < 0 ? SwipeDirection.Left : SwipeDirection.Right;
                else
                    Swipe = direction.y < 0 ? SwipeDirection.Down : SwipeDirection.Up;
            }

            _startTime = 0f;
        }
    }
}