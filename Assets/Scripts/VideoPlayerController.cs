using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerController : MonoBehaviour {

    VideoPlayer videoPlayer;

    [Header("Buttons")]

    [SerializeField]
    Button buttonRewind;

    [SerializeField]
    Button buttonRewind10s;

    [SerializeField]
    Button buttonPlay;

    [SerializeField]
    Button buttonForward10s;

    [SerializeField]
    Button buttonForward;

    [Header("Labels")]
    [SerializeField]
    TMP_Text labelElapsedTime;

    [SerializeField]
    TMP_Text labelTotalTime;

    [Header("Icon")]

    [SerializeField]
    Image iconPlay;

    [SerializeField]
    Sprite spritePlay;

    [SerializeField]
    Sprite spritePause;

    [SerializeField]
    Slider timeline;

    void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
        timeline.minValue = 0;
        timeline.maxValue = 0;
        timeline.value = 0;
    }

    void OnEnable() {
        videoPlayer.prepareCompleted += OnPrepareCompleted;
    }

    void OnDisable() {
        videoPlayer.prepareCompleted -= OnPrepareCompleted;
    }

    // Start is called before the first frame update
    void Start() {
        videoPlayer.Prepare();
        UpdateControls();
        InitTimeline();
    }

    // Update is called once per frame
    void Update() {
        timeline.value = videoPlayer.frame;
        labelElapsedTime.text = FormatTime(videoPlayer.time);
        UpdateControls();
    }
    

    private void OnPrepareCompleted(VideoPlayer source) {
        UpdateControls();
        InitTimeline();
    }

    void InitTimeline() {
        timeline.value = 0;
        timeline.minValue = 0;
        timeline.maxValue = videoPlayer.frameCount;
        labelElapsedTime.text = FormatTime(0.0f);
        labelTotalTime.text = FormatTime(videoPlayer.length);
    }

    void UpdateControls() {
        buttonRewind.interactable = videoPlayer.isPrepared;
        buttonRewind10s.interactable = videoPlayer.isPrepared;
        buttonPlay.interactable = videoPlayer.isPrepared;
        buttonForward10s.interactable = videoPlayer.isPrepared;
        buttonForward.interactable = videoPlayer.isPrepared;

        iconPlay.sprite = videoPlayer.isPlaying ? spritePause : spritePlay;
    }

    string FormatTime(double timeInSeconds) {
        TimeSpan t = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    public void StepForward() {
        videoPlayer.frame = Math.Min(videoPlayer.frame + Convert.ToInt64(videoPlayer.frameRate * 10), Convert.ToInt64(videoPlayer.frameCount));
    }

    public void StepBackward() {
        videoPlayer.frame = Math.Max(videoPlayer.frame - Convert.ToInt64(videoPlayer.frameRate * 10), 0);
    }

    public void ButtonPlayClicked() {
        if (videoPlayer.isPlaying) {
            videoPlayer.Pause();
        } else {
            videoPlayer.Play();
        }
    }
}
