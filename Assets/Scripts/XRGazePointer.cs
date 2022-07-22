using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XRGazePointer : PointerInputModule {
    [SerializeField]
    GameObject selectionProgressIndicator;

    PointerEventData pointerEventData;

    GameObject currGazeTarget = null;

    Coroutine selectionInProgressRoutine;
    bool selectionInProgress;

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
        ProcessSelection(pointerEventData);
    }

    void ProcessSelection(PointerEventData pointerEventData) {
        if (currGazeTarget != pointerEventData.pointerCurrentRaycast.gameObject) {
            if (selectionInProgress) {
                StopCoroutine(selectionInProgressRoutine);
                EndSelection();
            }
            if (pointerEventData.pointerCurrentRaycast.isValid) {
                selectionInProgressRoutine = StartCoroutine(StartSelection());
            }
        }
        currGazeTarget = pointerEventData.pointerCurrentRaycast.isValid ?
            pointerEventData.pointerCurrentRaycast.gameObject :
            null;
    }

    IEnumerator StartSelection() {
        selectionInProgress = true;
        selectionProgressIndicator.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        EndSelection();
        currGazeTarget.GetComponent<Button>()?.onClick.Invoke();
    }

    void EndSelection() {
        selectionInProgress = false;
        selectionProgressIndicator.SetActive(false);
    }

}
    