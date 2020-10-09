using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarUI : MonoBehaviour{    
    public void SetSize(float sizeNormalized) {
        Transform bar = transform.Find("Bar");
        if (bar != null) {
            bar.localScale = new Vector3(sizeNormalized, bar.localScale.y, 1);
        }
    }
}
