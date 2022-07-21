using System.Collections;
using System.Collections.Generic;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Management;

public class XRModeController : MonoBehaviour {

    public static XRModeController Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private float defaultFieldOfView = 60.0f;
    private Camera mainCamera;

    public UnityEvent XRModeEnteredEvent = new UnityEvent();
    public UnityEvent XRModeExitedEvent = new UnityEvent();

    private bool IsScreenTouched {
        get => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private bool IsXRModeEnabled {
        get => XRGeneralSettings.Instance.Manager.isInitializationComplete;
    }

    public void Start() {
        mainCamera = Camera.main;
        defaultFieldOfView = mainCamera.fieldOfView;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
        if (!Api.HasDeviceParams()) {
            Api.ScanDeviceParams();
        }
    }

    public void Update() {
        if (IsXRModeEnabled) {
            if (Api.IsCloseButtonPressed) {
                ExitXR();
            }

            if (Api.IsGearButtonPressed) {
                Api.ScanDeviceParams();
            }

            Api.UpdateScreenParams();
        } else {
            if (IsScreenTouched) {
                EnterXR();
            }
        }
    }

    public void EnterXR() {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams()) {
            Api.ReloadDeviceParams();
        }
        XRModeEnteredEvent.Invoke();
    }

    public void ExitXR() {
        StopXR();
        XRModeExitedEvent.Invoke();
    }

    private IEnumerator StartXR() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        if (XRGeneralSettings.Instance.Manager.activeLoader == null) {
            Debug.LogError("Initializing XR Failed.");
        } else {
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    private void StopXR() {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        mainCamera.ResetAspect();
        mainCamera.fieldOfView = defaultFieldOfView;
    }
}
