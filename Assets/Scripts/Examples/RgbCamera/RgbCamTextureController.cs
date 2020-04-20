using System;
using System.Collections;
using NRKernal;
using NRKernal.Record;
using UnityEngine;

namespace Examples.RgbCamera
{
    public class RgbCamTextureController : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Play,
            Pause
        }

        [SerializeField] private NRPreviewer previewer;

        private Texture _defaultTexture;
        private NRRGBCamTexture _texture;

        private State _state;

        private void SetState(State newState)
        {
            _state = newState;

            switch (newState)
            {
                case State.Idle:
                    previewer.SetData(_defaultTexture, false);
                    break;
                case State.Play:
                case State.Pause:
                    previewer.SetData(_texture.GetTexture(), _texture.IsPlaying);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            _defaultTexture = previewer.PreviewScreen.texture;
            _texture = new NRRGBCamTexture();
            SetState(State.Idle);
        }

        private IEnumerator Play()
        {
            _texture.Play();
            SetState(State.Play);
            yield break;
        }

        private IEnumerator Pause()
        {
            _texture.Pause();
            SetState(State.Pause);
            yield break;
        }

        private IEnumerator Stop()
        {
            _texture.Stop();
            SetState(State.Idle);
            yield break;
        }

        private void OnDisable()
        {
            Stop().MoveNext();
        }

        private void Update()
        {
            var deltaTouch = NRInput.GetDeltaTouch();
            if (deltaTouch.y > float.Epsilon) return;

            var isSwipeRight = deltaTouch.x > float.Epsilon;
            var isSwipeLeft = deltaTouch.x < -float.Epsilon;

            switch (_state)
            {
                case State.Idle:
                    if (isSwipeRight) StartCoroutine(nameof(Play));
                    break;
                case State.Play:
                    if (isSwipeLeft) StartCoroutine(nameof(Stop));
                    else if (NRInput.GetButtonUp(ControllerButton.TRIGGER)) StartCoroutine(nameof(Pause));
                    break;
                case State.Pause:
                    if (isSwipeLeft) StartCoroutine(nameof(Stop));
                    else if (NRInput.GetButtonUp(ControllerButton.TRIGGER)) StartCoroutine(nameof(Play));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}