using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] GameObject explosion;
    [SerializeField] int damage = 125;
    float damageRange = 7;
    public void MoveBullet(Vector3 target, float initialVelocity) {
        Rigidbody bulletRigidBody = GetComponent<Rigidbody>();    
        Vector3 finalVelocity = transform.forward * initialVelocity;
        //Fire
        bulletRigidBody.AddForce(finalVelocity * bulletRigidBody.mass, ForceMode.Impulse);        
    }        

    void OnTriggerEnter(Collider other) {      
        if (other.tag == "Desert") {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject[] listEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in listEnemy) {
                if (Vector3.Distance(enemy.transform.position, transform.position) < damageRange) {
                    enemy.GetComponent<IDamageable>().GetHit(damage);
                }
            }
        }        
    }
}
