using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {
    int damage = 500;
    void Start() {
        
    }
    void Update() {
        transform.Translate(transform.forward * 10 * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other) {        
        IDamageable obj = other.gameObject.GetComponent<IDamageable>();
        if (obj != null) {
            obj.GetHit(damage);
            Destroy(gameObject);
        }
    }
}
