using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Building, IAttackable {            
    [SerializeField] float attackSpeed = 2f;
    [SerializeField] GameObject projectile;
    Transform shotPoint;
    GameObject enemy;
    Coroutine attackCoroutine;
    void Start() {
        GetAttribute();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        shotPoint = transform.Find("Shot Point");
    }
    void Update() {
        SetTarget();
        if (attackCoroutine == null) attackCoroutine = StartCoroutine(Attack());
    }
    void SetTarget() {
        if (enemy == null) enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    public IEnumerator Attack() {
        yield return new WaitForSeconds(attackSpeed);
        if (enemy != null) {
            Instantiate(projectile, shotPoint.position, Quaternion.identity);            
        }
        attackCoroutine = null;
    }
}
