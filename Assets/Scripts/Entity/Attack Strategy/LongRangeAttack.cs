using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttack : MonoBehaviour, IAttackBehavior {
    float lastAttackTime = 0f;
    const float attackSpeed = 2f;
    public GameObject projectile;
    public Transform setpoint;
    
    public bool Attack(GameObject objective, int damage) {
        if (Time.time > lastAttackTime + attackSpeed) {
            transform.LookAt(objective.transform);
            GameObject pjt = Instantiate(projectile, setpoint.position, Quaternion.identity);
            pjt.transform.LookAt(objective.transform);
            lastAttackTime = Time.time;
            return true;
        }
        return false;
    }
}
