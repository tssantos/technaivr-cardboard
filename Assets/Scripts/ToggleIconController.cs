using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleIconController : MonoBehaviour {
    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite spriteNormal;

    [SerializeField]
    Sprite spriteSelected;

    Toggle toggle;

    private void OnEnable() {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
        icon.sprite = toggle.isOn ? spriteNormal : spriteSelected;
    }

    void OnValueChanged(bool value) {
        icon.sprite = toggle.isOn ? spriteNormal : spriteSelected;
    }
}
