using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour {

    [SerializeField] int damage;
    GameObject target;
    Vector3 lastAttackPoint;
    void Start() {
        damage = 200;
        target = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update(){
        MoveOrb();
    }
    public void MoveOrb() {
        if (target != null){
            transform.LookAt(target.transform);
            transform.Translate(transform.forward * 10 * Time.deltaTime, Space.World);
            lastAttackPoint = target.transform.position;
        }
        else{
            transform.LookAt(lastAttackPoint);
            transform.Translate(transform.forward * 10 * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, lastAttackPoint) < 1) Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (target == other.gameObject) {            
            target.GetComponent<IDamageable>().GetHit(damage);
            Destroy(gameObject);
        }        
    }
}
