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

            targets[_activeIndex].SetActive(false);

            switch (_touchHelper.Flick)
            {
                case FlickDirection.Down:
                    _activeIndex = ++_activeIndex < targets.Length ? _activeIndex : 0;
                    break;
                case FlickDirection.Up:
                    _activeIndex = --_activeIndex >= 0 ? _activeIndex : targets.Length - 1;
                    break;
                case FlickDirection.None:
                case FlickDirection.Left:
                case FlickDirection.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            targets[_activeIndex].SetActive(true);
        }
    }
}