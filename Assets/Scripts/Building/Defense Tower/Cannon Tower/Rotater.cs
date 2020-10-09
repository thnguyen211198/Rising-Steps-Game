using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {
    [SerializeField] GameObject bullet;    
    [SerializeField] int muzzleVelocity = 35;
    float rotationSpeed = 2;
    Transform cannon;
    Transform barrel;

    public Transform Barrel { get => barrel; }

    void Start() {
        barrel = transform.Find("Barrel");
        cannon = transform.Find("cannon");
    }
    public void LookAt(Vector3 target) {
        var rotation = RotateToEnemy(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);        
    }
    public void Shoot(Vector3 target, float initialVelocity) {
        var rotation = RotateToEnemy(target);
        Bullet b = Instantiate(bullet, barrel.position, Quaternion.identity).GetComponent<Bullet>();        
        b.transform.rotation = cannon.rotation;
        b.MoveBullet(target, initialVelocity);
    }

    Quaternion RotateToEnemy(Vector3 target) {
        Vector3 from = transform.position;
        Vector3 to = target;
        to.y = transform.position.y;
        var rotation = Quaternion.LookRotation(to - from);
        return rotation;
    }
}
