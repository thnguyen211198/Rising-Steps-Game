using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeAttack : MonoBehaviour, IAttackBehavior{
    protected float lastAttackTime = 0f;
    public const float attackSpeed = 2f;
    public bool Attack(GameObject objective, int damage) {
        if (Time.time > lastAttackTime + attackSpeed) {
            transform.LookAt(objective.transform);
            lastAttackTime = Time.time;
            objective.GetComponent<IDamageable>().GetHit(damage);
            return true;
        }
        return false;
    }
}
