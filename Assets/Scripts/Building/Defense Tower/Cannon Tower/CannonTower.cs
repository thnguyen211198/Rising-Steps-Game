using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Building, IAttackable {
    [SerializeField] GameObject fireEffect;
    [SerializeField] float attackSpeed = 3f;    
    const float angle = 35 * Mathf.Deg2Rad;
    Rotater rotater;    
    GameObject enemy;
    Vector3 target;
    float enemySpeed;
    Vector3 observation;
    const float timeToGetSpeedSample = 1f;
    Coroutine estimateEnemySpeedCoroutine;
    Coroutine attackCoroutine;
    void Start() {
        GetAttribute();
        rotater = GetComponentInChildren<Rotater>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");        
        if (enemy != null) observation = enemy.transform.position;
        target = new Vector3();
    }
    void Update() {
        SetTarget();
        if (estimateEnemySpeedCoroutine == null) estimateEnemySpeedCoroutine = StartCoroutine(EstimateEnemySpeed());
        if (attackCoroutine == null) attackCoroutine = StartCoroutine(Attack());
    }
    void SetTarget() {
        if (enemy == null) {
            enemy = GameObject.FindGameObjectWithTag("Enemy");            
            if (enemy != null) observation = enemy.transform.position;            
        }
        else {            
            float t = EstimateFlightTime(enemy.transform.position);
            float s = t * enemySpeed;
            Vector3 offset = s * enemy.transform.forward;
            target.Set(enemy.transform.position.x + offset.x, enemy.transform.position.y, enemy.transform.position.z + offset.z);
            rotater.LookAt(target);
        }
    }
    float EstimateFlightTime(Vector3 destination) {        
        Vector3 barrelPosition = rotater.Barrel.position;
        float d = PlanarDistance(barrelPosition, destination);
        float time = d / (EstimateBulletVelocity(destination) * Mathf.Cos(angle));
        return time;
    }
    float EstimateBulletVelocity(Vector3 target) {
        float g = Physics.gravity.magnitude;             
        Vector3 barrelPosition = rotater.Barrel.position;
        float d = PlanarDistance(barrelPosition, target);
        // Distance along the y axis between objects
        float yOffset = barrelPosition.y - target.y;
        float initialVelocity = Mathf.Sqrt((0.5f * g * Mathf.Pow(d, 2)) / (d * Mathf.Tan(angle) + yOffset)) / Mathf.Cos(angle);
        return initialVelocity;
    }
    float PlanarDistance(Vector3 source, Vector3 destination) {
        Vector3 planarSource = new Vector3(source.x, 0, source.z);
        Vector3 planarTarget = new Vector3(destination.x, 0, destination.z);
        return Vector3.Distance(planarSource, planarTarget);
    }
    IEnumerator EstimateEnemySpeed() {
        yield return new WaitForSeconds(timeToGetSpeedSample);
        if (enemy != null){
            enemySpeed = Vector3.Distance(enemy.transform.position, observation) / timeToGetSpeedSample;
            observation = enemy.transform.position;            
        }
        else enemySpeed = 0;        
        estimateEnemySpeedCoroutine = null;
    }
    public IEnumerator Attack() {        
        yield return new WaitForSeconds(attackSpeed);
        if (enemy != null && target != null) {
            Instantiate(fireEffect, rotater.Barrel.position, Quaternion.identity);
            rotater.Shoot(target, EstimateBulletVelocity(target));
        }
        attackCoroutine = null;
    }
}
