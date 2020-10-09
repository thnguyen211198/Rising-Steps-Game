using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBarUI : MonoBehaviour {
    public void SetSize(float sizeNormalized) {
        GetComponent<Image>().fillAmount = sizeNormalized;
    }
}
