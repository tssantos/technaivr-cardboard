using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour {

    [System.Serializable]
    public enum RotationState {
        Stopped,
        RotatingCW,
        RotatingCCW
    }
    [Range(0.0f, 360.0f)]
    float rotationSpeed = 90.0f;

    [SerializeField]
    RotationState state = RotationState.Stopped;

    // Update is called once per frame
    void Update() {
        float deltaTime = Time.deltaTime;
        float rotationAngle = deltaTime * rotationSpeed;
        switch(state) {
            case RotationState.RotatingCCW:
                rotationAngle *= -1.0f;
            break;
            case RotationState.RotatingCW:
                rotationAngle *= 1.0f;
                break;
            default:
                rotationAngle *= 0.0f;
                break;
        }
        this.transform.RotateAround(this.transform.position, Vector3.up, rotationAngle);
    }

    public void StopRotating() {
        state = RotationState.Stopped;
    }

    public void StartRotatingCCW() {
        state = RotationState.RotatingCCW;
    }

    public void StartRotatingCW() {
        state = RotationState.RotatingCW;
    }

}
