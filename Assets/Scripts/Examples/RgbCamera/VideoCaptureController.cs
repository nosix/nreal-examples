using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using NRKernal;
using NRKernal.Record;
using UnityEngine;

namespace Examples.RgbCamera
{
    public class VideoCaptureController : MonoBehaviour
    {
        private enum State
        {
            Disabled,
            Idle,
            Record
        }

        [SerializeField] private NRPreviewer previewer;

        public string videoDirectoryPath;

        private Texture _defaultTexture;
        private NRVideoCapture _videoCapture;

        private State _state;

        private string VideoFilePath
        {
            get
            {
                if (!Directory.Exists(videoDirectoryPath)) Directory.CreateDirectory(videoDirectoryPath);
                var timeStamp = Time.time.ToString(CultureInfo.InvariantCulture)
                    .Replace(".", "");
                return Path.Combine(videoDirectoryPath, $"video_{timeStamp}.mp4");
            }
        }

        private void SetState(State newState)
        {
            _state = newState;

            switch (newState)
            {
                case State.Disabled:
                    previewer.SetData(_defaultTexture, false);
                    break;
                case State.Idle:
                case State.Record:
                    previewer.SetData(_videoCapture.PreviewTexture, _videoCapture.IsRecording);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            _defaultTexture = previewer.PreviewScreen.texture;
            SetState(State.Disabled);
        }

        private void Start()
        {
            CreateVideoCapture();
        }

        private void CreateVideoCapture()
        {
            NRVideoCapture.CreateAsync(false, captureObject =>
            {
                if (captureObject == null)
                {
                    Debug.LogError("Can't get a NRVideoCapture object.");
                    return;
                }

                var resolution = NRVideoCapture.SupportedResolutions
                    .OrderByDescending(r => r.width * r.height)
                    .First();

                var frameRate = NRVideoCapture.GetSupportedFrameRatesForResolution(resolution)
                    .OrderByDescending(fps => fps)
                    .First();

                var cameraParameters = new CameraParameters
                {
                    hologramOpacity = 0.0f,
                    frameRate = frameRate,
                    cameraResolutionWidth = resolution.width,
                    cameraResolutionHeight = resolution.height,
                    pixelFormat = CapturePixelFormat.BGRA32,
                    blendMode = BlendMode.Blend
                };

                _videoCapture = captureObject;
                _videoCapture.StartVideoModeAsync(
                    cameraParameters, NRVideoCapture.AudioState.ApplicationAndMicAudio, result => SetState(State.Idle));
            });
        }

        private IEnumerator Record()
        {
            if (_videoCapture == null)
            {
                Debug.LogError("The NRVideoCapture has not been created.");
                yield break;
            }

            var filePath = VideoFilePath;
            _videoCapture.StartRecordingAsync(filePath, result => SetState(State.Record));
            Debug.Log($"Record video to {filePath}");
        }

        private IEnumerator Stop()
        {
            if (_videoCapture == null)
            {
                Debug.LogError("The NRVideoCapture has not been created.");
                yield break;
            }

            _videoCapture.StopRecordingAsync(result => SetState(State.Idle));
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Dispose()
        {
            _videoCapture?.Dispose();
            _videoCapture = null;
            SetState(State.Disabled);
        }

        private void Update()
        {
            var deltaTouch = NRInput.GetDeltaTouch();
            if (deltaTouch.y > float.Epsilon) return;

            var isSwipeRight = deltaTouch.x > float.Epsilon;
            var isSwipeLeft = deltaTouch.x < -float.Epsilon;

            switch (_state)
            {
                case State.Disabled:
                    break;
                case State.Idle:
                    if (isSwipeRight) StartCoroutine(nameof(Record));
                    break;
                case State.Record:
                    if (isSwipeLeft) StartCoroutine(nameof(Stop));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}