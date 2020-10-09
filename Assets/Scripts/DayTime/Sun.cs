using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    void Update() {
        transform.RotateAround(Vector3.zero, Vector3.forward, 30f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
