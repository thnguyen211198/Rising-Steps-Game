using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed;
    KeyCode moveLeft = KeyCode.LeftArrow;
    KeyCode moveRight = KeyCode.RightArrow;
    KeyCode moveUp = KeyCode.UpArrow;
    KeyCode moveDown = KeyCode.DownArrow;
    KeyCode zoomIn = KeyCode.X;
    KeyCode zoomOut = KeyCode.Z;
    void Update() {
        Transform thisTransform = gameObject.transform;
        if (Input.GetKey(moveLeft)) {
            thisTransform.position = new Vector3(thisTransform.position.x - moveSpeed * Time.deltaTime, thisTransform.position.y, thisTransform.position.z);
        }
        if (Input.GetKey(moveRight)) {
            thisTransform.position = new Vector3(thisTransform.position.x + moveSpeed * Time.deltaTime, thisTransform.position.y, thisTransform.position.z);
        }
        if (Input.GetKey(moveUp)) {
            thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z + moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(moveDown)) {
            thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(zoomOut)) {
            thisTransform.Translate(-Vector3.forward);
        }
        if (Input.GetKey(zoomIn)) {
            thisTransform.Translate(Vector3.forward);
        }
    }
}
