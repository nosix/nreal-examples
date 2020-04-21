using System;
using System.Linq;
using UnityEngine;

namespace Examples.RgbCamera
{
    public class ModeSelector : MonoBehaviour
    {
        public GameObject[] targets;

        private readonly ControllerTouchHelper _touchHelper = new ControllerTouchHelper();

        private int _activeIndex;

        private void OnEnable()
        {
            foreach (var target in targets) target.SetActive(false);
            targets.First()?.SetActive(true);
        }
        
        private void Update()
        {
            _touchHelper.Update();

            if (_touchHelper.Swipe == SwipeDirection.None) return;

            switch (_touchHelper.Swipe)
            {
                case SwipeDirection.Down:
                    targets[_activeIndex].SetActive(false);
                    _activeIndex = ++_activeIndex < targets.Length ? _activeIndex : 0;
                    targets[_activeIndex].SetActive(true);
                    break;
                case SwipeDirection.Up:
                    targets[_activeIndex].SetActive(false);
                    _activeIndex = --_activeIndex >= 0 ? _activeIndex : targets.Length - 1;
                    targets[_activeIndex].SetActive(true);
                    break;
                case SwipeDirection.None:
                case SwipeDirection.Left:
                case SwipeDirection.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}