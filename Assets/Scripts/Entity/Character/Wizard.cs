using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : SoldierBehavior {    
    public GameObject magic;
    public Transform setpoint;
    void Start() {
        GetAttribute();
        attackBehavior = gameObject.AddComponent<LongRangeAttack>();
        (attackBehavior as LongRangeAttack).projectile = magic;
        (attackBehavior as LongRangeAttack).setpoint = setpoint;
        attackRange = 23;
        attackSpeed = 4f;
        damage = 300;
        agent.speed = 3;
        healthPoint = 300;
    }
    void Update() {
        SetTarget();
        if (setStatusCoroutine == null) setStatusCoroutine = StartCoroutine(Act());
    }
    
}
