# Native API (NRSDK 1.2.1)

## kernel32

### NativeEmulator.cs

- bool FreeLibrary
    - IntPtr hModule

### NativeInterface.cs

- bool SetDllDirectory
    - string lpPathName

## NativeConstants.NRNativeLibrary

### NativeEmulator.cs

- NativeResult NRSIMTrackingCreate
    - ref UInt64 out_tracking_handle

- NativeResult NRSIMTrackingSetHeadTrackingPose
    - UInt64 tracking_handle
    - ref NativeVector3f position
    - ref NativeVector4f rotation

- NativeResult NRSIMTrackingUpdateTrackableImageData 
    - UInt64 tracking_handle
    - ref NativeVector3f center_pos
    - ref NativeVector4f center_rotation
    - float extent_x
    - float extent_z
    - UInt32 identifier
    - int state

- NativeResult NRSIMTrackingUpdateTrackablePlaneData
    - UInt64 tracking_handle
    - ref NativeVector3f center_pos
    - ref NativeVector4f center_rotation
    - float extent_x
    - float extent_z
    - UInt32 identifier
    - int state

- NativeResult NRSIMControllerCreate
    - ref UInt64 out_controller_handle

- NativeResult NRSIMControllerDestroyAll

- NativeResult NRSIMControllerSetTimestamp
    - UInt64 controller_handle
    - UInt64 timestamp

- NativeResult NRSIMControllerSetPosition
    - UInt64 controller_handle
    - ref NativeVector3f position

- NativeResult NRSIMControllerSetRotation
    - UInt64 controller_handle
    - ref NativeVector4f rotation

- NativeResult NRSIMControllerSetAccelerometer
    - UInt64 controller_handle
    - ref NativeVector3f accel

- NativeResult NRSIMControllerSetButtonState
    - UInt64 controller_handle
    - Int32 buttonState

- NativeResult NRSIMControllerSetIsTouching
    - UInt64 controller_handle
    - bool isTouching

- NativeResult NRSIMControllerSetTouchPoint
    - UInt64 controller_handle
    - ref NativeVector3f point

- NativeResult NRSIMControllerSubmit
    - UInt64 controller_handle

### NRDevice.cs

- NativeResult NRSDKInitSetAndroidActivity
    - IntPtr android_activity

### NativeCamera.cs

- NativeResult NRRGBCameraImageGetRawData
    - UInt64 rgb_camera_handle
    - UInt64 rgb_camera_image_handle
    - ref IntPtr out_image_raw_data
    - ref UInt32 out_image_raw_data_size

- NativeResult NRRGBCameraImageGetResolution
    - UInt64 rgb_camera_handle
    - UInt64 rgb_camera_image_handle
    - ref NativeResolution out_image_resolution

- NativeResult NRRGBCameraImageGetHMDTimeNanos
    - UInt64 rgb_camera_handle
    - UInt64 rgb_camera_image_handle
    - ref UInt64 out_image_hmd_time_nanos

- NativeResult NRRGBCameraCreate
    - ref UInt64 out_rgb_camera_handle

- NativeResult NRRGBCameraDestroy
    - UInt64 rgb_camera_handle

- NativeResult NRRGBCameraSetCaptureCallback
    - UInt64 rgb_camera_handle
    - NRRGBCameraImageCallback image_callback
    - UInt64 userdata

- NativeResult NRRGBCameraSetImageFormat
    - UInt64 rgb_camera_handle
    - CameraImageFormat format

- NativeResult NRRGBCameraStartCapture
    - UInt64 rgb_camera_handle

- NativeResult NRRGBCameraStopCapture
    - UInt64 rgb_camera_handle

- NativeResult NRRGBCameraImageDestroy
    - UInt64 rgb_camera_handle
    - UInt64 rgb_camera_image_handle

### NativeConfigration.cs

- NativeResult NRConfigCreate
    - UInt64 session_handle
    - ref UInt64 out_config_handle

- NativeResult NRConfigDestroy
    - UInt64 session_handle
    - UInt64 config_handle

- NativeResult NRConfigGetTrackablePlaneFindingMode
    - UInt64 session_handle
    - UInt64 config_handle
    - ref TrackablePlaneFindingMode out_trackable_plane_finding_mode

- NativeResult NRConfigSetTrackablePlaneFindingMode
    - UInt64 session_handle
    - UInt64 config_handle
    - TrackablePlaneFindingMode trackable_plane_finding_mode

- NativeResult NRConfigGetTrackableImageDatabase
    - UInt64 session_handle
    - UInt64 config_handle
    - ref UInt64 out_trackable_image_database_handle

- NativeResult NRConfigSetTrackableImageDatabase
    - UInt64 session_handle
    - UInt64 config_handle
    - UInt64 trackable_image_database_handle

### NativeController.cs

- NativeResult NRControllerCreate
    - ref UInt64 out_controller_handle

- NativeResult NRControllerStart
    - UInt64 controller_handle

- NativeResult NRControllerPause
    - UInt64 controller_handle

- NativeResult NRControllerResume
    - UInt64 controller_handle

- NativeResult NRControllerStop
    - UInt64 controller_handle

- NativeResult NRControllerDestroy
    - UInt64 controller_handle

- NativeResult NRControllerGetCount
    - UInt64 controller_handle
    - ref int out_controller_count

- NativeResult NRControllerGetAvailableFeatures
    - UInt64 controller_handle
    - int controller_index
    - ref uint out_controller_available_features

- NativeResult NRControllerGetType
    - UInt64 controller_handle
    - int controller_index
    - ref ControllerType out_controller_type

- NativeResult NRControllerRecenter
    - UInt64 controller_handle
    - int controller_index

- NativeResult NRControllerStateCreate
    - UInt64 controller_handle
    - int controller_index
    - ref UInt64 out_controller_state_handle

- NativeResult NRControllerStateUpdate
    - UInt64 controller_state_handle

- NativeResult NRControllerStateDestroy
    - UInt64 controller_state_handle

- NativeResult NRControllerHapticVibrate
    - UInt64 controller_handle
    - int controller_index
    - Int64 duration
    - float frequency
    - float amplitude

- NativeResult NRControllerStateGetConnectionState
    - UInt64 controller_state_handle
    - ref ControllerConnectionState out_controller_connection_state

- NativeResult NRControllerStateGetBatteryLevel
    - UInt64 controller_state_handle
    - ref int out_controller_battery_level

- NativeResult NRControllerStateGetCharging
    - UInt64 controller_state_handle
    - ref int out_controller_charging

- NativeResult NRControllerStateGetPose
    - UInt64 controller_state_handle
    - ref NativeMat4f out_controller_pose

- NativeResult NRControllerStateGetGyro
    - UInt64 controller_state_handle
    - ref NativeVector3f out_controller_gyro

- NativeResult NRControllerStateGetAccel
    - UInt64 controller_state_handle
    - ref NativeVector3f out_controller_accel

- NativeResult NRControllerStateGetMag
    - UInt64 controller_state_handle
    - ref NativeVector3f out_controller_mag

- NativeResult NRControllerStateGetButtonState
    - UInt64 controller_state_handle
    - ref uint out_controller_button_state

- NativeResult NRControllerStateGetButtonUp
    - UInt64 controller_state_handle
    - ref uint out_controller_button_up

- NativeResult NRControllerStateGetButtonDown
    - UInt64 controller_state_handle
    - ref uint out_controller_button_down

- NativeResult NRControllerStateTouchState
    - UInt64 controller_state_handle
    - ref uint out_controller_touch_state

- NativeResult NRControllerStateGetTouchUp
    - UInt64 controller_state_handle
    - ref uint out_controller_touch_up

- NativeResult NRControllerStateGetTouchDown
    - UInt64 controller_state_handle
    - ref uint out_controller_touch_down

- NativeResult NRControllerStateGetTouchPose
    - UInt64 controller_state_handle
    - ref NativeVector2f out_controller_touch_pose

- NativeResult NRControllerSetHeadPose
    - UInt64 controller_handle
    - ref NativeMat4f out_controller_pose

### NativeHeadTracking.cs

- NativeResult NRHeadTrackingCreate
    - UInt64 tracking_handle
    - ref UInt64 outHeadTrackingHandle

- NativeResult NRTrackingGetHMDTimeNanos
    - UInt64 tracking_handle
    - ref UInt64 out_hmd_time_nanos

- NativeResult NRHeadTrackingGetRecommendPredictTime
    - UInt64 tracking_handle
    - UInt64 head_tracking_handle
    - ref UInt64 out_predict_time_nanos

- NativeResult NRHeadTrackingAcquireTrackingPose
    - UInt64 sessionHandle
    - UInt64 head_tracking_handle
    - UInt64 hmd_time_nanos
    - ref UInt64 out_tracking_pose_handle

- NativeResult NRTrackingPoseGetPose
    - UInt64 tracking_handle
    - UInt64 tracking_pose_handle
    - ref NativeMat4f out_pose

- NativeResult NRTrackingPoseGetTrackingReason
    - UInt64 tracking_handle
    - UInt64 tracking_pose_handle
    - ref LostTrackingReason out_tracking_reason

- NativeResult NRTrackingPoseDestroy
    - UInt64 tracking_handle
    - UInt64 tracking_pose_handle

- NativeResult NRHeadTrackingDestroy
    - UInt64 tracking_handle
    - UInt64 head_tracking_handle

### NativeHMD.cs

- NativeResult NRHMDCreate
    - ref UInt64 out_hmd_handle

- NativeResult NRHMDGetEyePoseFromHead
    - UInt64 hmd_handle
    - int eye
    - ref NativeMat4f outEyePoseFromHead

- NativeResult NRHMDGetEyeFov
    - UInt64 hmd_handle
    - int eye
    - ref NativeFov4f out_eye_fov

- NativeResult NRHMDGetEyeResolution
    - UInt64 hmd_handle
    - int eye
    - ref NativeResolution out_eye_resolution

- NativeResult NRHMDDestroy
    - UInt64 hmd_handle

### NativeMultiDisplay.cs

- NativeResult NRDisplayCreate
    - ref UInt64 out_display_handle

- NativeResult NRDisplayPause
    - UInt64 display_handle

- NativeResult NRDisplayResume
    - UInt64 display_handle

- NativeResult NRDisplaySetMainDisplayTexture
    - UInt64 display_handle
    - IntPtr controller_texture

- NativeResult NRDisplayDestroy
    - UInt64 display_handle

### NativePlane.cs

- NativeResult NRTrackablePlaneGetType
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref TrackablePlaneType out_plane_type

- NativeResult NRTrackablePlaneGetCenterPose
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref NativeMat4f out_center_pose

- NativeResult NRTrackablePlaneGetExtentX
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref float out_extent_x

- NativeResult NRTrackablePlaneGetExtentZ
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref float out_extent_z

- NativeResult NRTrackablePlaneGetPolygonSize
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref int out_polygon_size

- NativeResult NRTrackablePlaneGetPolygon
    - UInt64 session_handle
    - UInt64 trackable_handle
    - IntPtr out_polygon

### NativeRenderring.cs

- NativeResult NRRenderingCreate
    - ref UInt64 out_rendering_handle

- NativeResult NRRenderingStart
    - UInt64 rendering_handle

- NativeResult NRRenderingDestroy
    - UInt64 rendering_handle

- NativeResult NRRenderingPause
    - UInt64 rendering_handle

- NativeResult NRRenderingResume
    - UInt64 rendering_handle

- NativeResult NRRenderingInitSetAndroidSurface
    - UInt64 rendering_handle
    - IntPtr android_surface

- NativeResult NRRenderingDoRender
    - UInt64 rendering_handle
    - IntPtr left_eye_texture
    - IntPtr right_eye_texture
    - ref NativeMat4f head_pose

### NativeTrackable.cs

- NativeResult NRTrackableListCreate
    - UInt64 session_handle
    - ref UInt64 out_trackable_list_handle

- NativeResult NRTrackableListDestroy
    - UInt64 session_handle
    - UInt64 out_trackable_list_handle

- NativeResult NRTrackableListGetSize
    - UInt64 session_handle
    - UInt64 trackable_list_handle
    - ref int out_list_size

- NativeResult NRTrackableListAcquireItem
    - UInt64 session_handle
    - UInt64 trackable_list_handle
    - int index
    - ref UInt64 out_trackable

- NativeResult NRTrackableGetIdentifier
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref UInt32 out_identifier

- NativeResult NRTrackableGetType
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref TrackableType out_trackable_type

- NativeResult NRTrackableGetTrackingState
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref TrackingState out_tracking_state

### NativeTrackableImage.cs

- NativeResult NRTrackableImageDatabaseCreate
    - UInt64 session_handle
    - ref UInt64 out_trackable_image_database_handle

- NativeResult NRTrackableImageDatabaseDestroy
    - UInt64 session_handle
    - UInt64 trackable_image_database_handle

- NativeResult NRTrackableImageDatabaseLoadDirectory
    - UInt64 session_handle
    - UInt64 trackable_image_database_handle
    - string trackable_image_database_directory

- NativeResult NRTrackableImageGetCenterPose
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref NativeMat4f out_center_pose

- NativeResult NRTrackableImageGetExtentX
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref float out_extent_x

- NativeResult NRTrackableImageGetExtentZ
    - UInt64 session_handle
    - UInt64 trackable_handle
    - ref float out_extent_z

### NativeTracking.cs

- NativeResult NRTrackingCreate
    - ref UInt64 out_tracking_handle

- NativeResult NRTrackingInitSetTrackingMode
    - UInt64 tracking_handle
    - TrackingMode tracking_mode

- NativeResult NRTrackingStart
    - UInt64 tracking_handle

- NativeResult NRTrackingDestroy
    - UInt64 tracking_handle

- NativeResult NRTrackingPause
    - UInt64 tracking_handle

- NativeResult NRTrackingResume
    - UInt64 tracking_handle

- NativeResult NRTrackingRecenter
    - UInt64 tracking_handle

- NativeResult NRTrackingUpdateTrackables
    - UInt64 tracking_handle
    - TrackableType trackable_type
    - UInt64 out_trackable_list_handle

### NativeVersion.cs

- NativeResult NRGetVersion
    - ref NRVersion out_version

## NRNativeEncodeLibrary

### NativeEncoder.cs

- int HWEncoderCreate
    - ref UInt64 out_encoder_handle

- int HWEncoderStart
    - UInt64 encoder_handle

- int HWEncoderSetConfigration
    - UInt64 encoder_handle
    - string config

- int HWEncoderUpdateSurface
    - UInt64 encoder_handle
    - IntPtr texture_id
    - UInt64 time_stamp

- int HWEncoderStop
    - UInt64 encoder_handle

- int HWEncoderDestroy
    - UInt64 encoder_handle