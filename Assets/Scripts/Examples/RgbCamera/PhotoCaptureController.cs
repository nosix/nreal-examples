using System;
using System.Collections;
using System.Linq;
using NRKernal;
using NRKernal.Record;
using UnityEngine;

namespace Examples.RgbCamera
{
    public class PhotoCaptureController : MonoBehaviour
    {
        private enum State
        {
            Closed,
            Opened
        }

        [SerializeField] private NRPreviewer previewer;

        private Texture2D _texture;
        private Texture _defaultTexture;
        private NRPhotoCapture _photoCapture;

        private State _state;

        private void SetState(State newState)
        {
            _state = newState;

            switch (newState)
            {
                case State.Closed:
                    previewer.SetData(_defaultTexture, false);
                    break;
                case State.Opened:
                    previewer.SetData(_texture, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            var resolution = NRPhotoCapture.SupportedResolutions
                .OrderByDescending(r => r.width * r.height)
                .First();
            _texture = new Texture2D(resolution.width, resolution.height);
            _defaultTexture = previewer.PreviewScreen.texture;
            SetState(State.Closed);
        }

        private IEnumerator Open()
        {
            if (_photoCapture != null)
            {
                Debug.LogError("The NRPhotoCapture has already been created.");
                yield break;
            }

            NRPhotoCapture.CreateAsync(false, captureObject =>
            {
                if (captureObject == null)
                {
                    Debug.LogError("Can't get a NRPhotoCapture object.");
                    return;
                }

                var cameraParameters = new CameraParameters
                {
                    cameraResolutionWidth = _texture.width,
                    cameraResolutionHeight = _texture.height,
                    pixelFormat = CapturePixelFormat.BGRA32,
                    blendMode = BlendMode.Blend
                };

                _photoCapture = captureObject;
                _photoCapture.StartPhotoModeAsync(cameraParameters, result => SetState(State.Opened));
            });
        }

        private IEnumerator Capture()
        {
            if (_photoCapture == null)
            {
                Debug.LogError("The NRPhotoCapture has not been created.");
                yield break;
            }

            _photoCapture.TakePhotoAsync((result, photoCaptureFrame) =>
                photoCaptureFrame.UploadImageDataToTexture(_texture));
        }

        private IEnumerator Close()
        {
            if (_photoCapture == null)
            {
                Debug.LogError("The NRPhotoCapture has not been created.");
                yield break;
            }

            _photoCapture.StopPhotoModeAsync(result => Dispose());
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Dispose()
        {
            _photoCapture?.Dispose(); // BUG Leak NRCaptureBehavior
            _photoCapture = null;
            SetState(State.Closed);
        }

        private void Update()
        {
            var deltaTouch = NRInput.GetDeltaTouch();
            if (deltaTouch.y > float.Epsilon) return;

            var isSwipeRight = deltaTouch.x > float.Epsilon;
            var isSwipeLeft = deltaTouch.x < -float.Epsilon;

            switch (_state)
            {
                case State.Closed:
                    if (isSwipeRight) StartCoroutine(nameof(Open));
                    break;
                case State.Opened:
                    if (isSwipeLeft) StartCoroutine(nameof(Close));
                    else if (NRInput.GetButtonUp(ControllerButton.TRIGGER)) StartCoroutine(nameof(Capture));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}