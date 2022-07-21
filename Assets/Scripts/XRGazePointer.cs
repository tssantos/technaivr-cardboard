using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XRGazePointer : PointerInputModule {

    PointerEventData pointerEventData;

    GameObject gazedObject;

    public override void Process() {
        HandleLook();
    }

    // Update is called once per frame
    void HandleLook() {
        if (pointerEventData == null) {
            pointerEventData = new PointerEventData(eventSystem);
        }
        pointerEventData.position = new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        pointerEventData.delta = Vector2.zero;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
        ProcessMove(pointerEventData);
    }
}
    