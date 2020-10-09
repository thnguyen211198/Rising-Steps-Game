using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : SoldierBehavior {    
    void Start(){
        GetAttribute();
        attackBehavior = gameObject.AddComponent<CloseRangeAttack>();
        attackRange = 1.5f;
        damage = 400;
        agent.speed = 4;
        healthPoint = 500;
    }
    void Update() {
        SetTarget();
        if (setStatusCoroutine == null) setStatusCoroutine = StartCoroutine(Act());
    }
}
