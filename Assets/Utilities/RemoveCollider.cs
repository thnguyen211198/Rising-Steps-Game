using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollider : MonoBehaviour{    
    void OnDrawGizmos(){
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) {
            DestroyImmediate(collider);            
        }
    }
}
