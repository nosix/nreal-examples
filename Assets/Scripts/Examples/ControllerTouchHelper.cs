using NRKernal;
using UnityEngine;

namespace Examples
{
    public enum FlickDirection
    {
        None,
        Left,
        Right,
        Down,
        Up
    }

    public class ControllerTouchHelper
    {
        private readonly float _flickSpeedThreshold;

        public FlickDirection Flick { get; private set; }

        private float _startTime;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public ControllerTouchHelper(float flickSpeedThreshold = 0.01f)
        {
            _flickSpeedThreshold = flickSpeedThreshold;
        }

        public void Update()
        {
            Flick = FlickDirection.None;

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

            var touchTime = Time.time - _startTime;
            var direction = _endPosition - _startPosition;
            if (direction.magnitude / touchTime >= _flickSpeedThreshold)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    Flick = direction.x < 0 ? FlickDirection.Left : FlickDirection.Right;
                else
                    Flick = direction.y < 0 ? FlickDirection.Down : FlickDirection.Up;
            }

            _startTime = 0f;
        }
    }
}