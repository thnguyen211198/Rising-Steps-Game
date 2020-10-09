using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Building, IAttackable {
    [SerializeField] float attackSpeed = 0.1f;
    [SerializeField] int damage = 100;
    [SerializeField] GameObject muzzleFlash;

    float rotationSpeed = 2;
    Transform theGuardTransform;
    Transform shotPoint;
    GameObject enemy;    
    Coroutine attackCoroutine;
    void Start() {
        GetAttribute();
        theGuardTransform = transform.Find("Guardian");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        shotPoint = theGuardTransform.Find("Shot Point");
        muzzleFlash = Instantiate(muzzleFlash, shotPoint.position, muzzleFlash.transform.rotation);
        muzzleFlash.transform.parent = theGuardTransform;
    }
    void Update() {
        SetTarget();        
        if (attackCoroutine == null) attackCoroutine = StartCoroutine(Attack());
    }
    void SetTarget() {
        if (enemy == null) {
            muzzleFlash.SetActive(false);
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        else {
            LookAt(enemy.transform.position);
            muzzleFlash.SetActive(true);
        }
    }
    public IEnumerator Attack() {
        yield return new WaitForSeconds(attackSpeed);
        if (enemy != null) {            
            enemy.GetComponent<IDamageable>().GetHit(damage);
        }
        attackCoroutine = null;
    }
    public void LookAt(Vector3 target) {
        var rotation = RotateToPosition(theGuardTransform.position, target);
        theGuardTransform.rotation = Quaternion.Slerp(theGuardTransform.rotation, rotation, Time.deltaTime * rotationSpeed);        
    }
    Quaternion RotateToPosition(Vector3 from, Vector3 to) {
        to.y = from.y;
        var rotation = Quaternion.LookRotation(to - from);
        return rotation;
    }
}
