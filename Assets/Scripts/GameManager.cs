using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        XRModeController.Instance.XRModeEnteredEvent.AddListener(OnXRModeEntered);
        XRModeController.Instance.XRModeExitedEvent.AddListener(OnXRModeExited);
    }

    void OnDisable() {
        XRModeController.Instance.XRModeEnteredEvent.RemoveListener(OnXRModeEntered);
        XRModeController.Instance.XRModeExitedEvent.RemoveListener(OnXRModeExited);

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnXRModeEntered() {
        Debug.Log("[THVR] GameManager::OnXRModeEntered");
    }

    void OnXRModeExited() {
        Debug.Log("[THVR] GameManager::OnXRModeEntered");
    }

    public void Clicked() {
        Debug.Log("[THVR] GameManager::Clicked");
    }
}
