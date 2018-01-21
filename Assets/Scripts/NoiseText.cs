using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseText : MonoBehaviour {

    public Color[] textColors;

    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    void LateUpdate() {
        int noiseLevel = Player.instance.noiseLevel;
        text.text = "Noise: ";
        if (noiseLevel == 0)
            text.text += "none";
        else if (noiseLevel == 1)
            text.text += "low";
        else if (noiseLevel == 2)
            text.text += "medium";
        else if (noiseLevel == 3)
            text.text += "high!";
        text.color = textColors[noiseLevel];
    }
}
