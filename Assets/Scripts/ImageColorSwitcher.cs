using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorSwitcher : MonoBehaviour {
    Image image;

    int colorIndex = 0;

    List<Color> colorsList = new List<Color> {
        Color.red,
        Color.green,
        Color.blue,
    };

    void Awake() {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start() {
        image.color = colorsList[colorIndex];
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SwitchColor() {
        colorIndex += 1;
        if (colorIndex >= colorsList.Count) {
            colorIndex = 0;
        }
        image.color = colorsList[colorIndex];
    }
}
