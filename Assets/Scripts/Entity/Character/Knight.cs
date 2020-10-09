using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : SoldierBehavior{    
    void Start() {
        GetAttribute();
        attackBehavior = gameObject.AddComponent<CloseRangeAttack>();
        attackRange = 1;
        damage = 250;
        agent.speed = 4;
        healthPoint = 900;
    }
    void Update() {
        SetTarget();
        if (setStatusCoroutine == null) setStatusCoroutine = StartCoroutine(Act());
    }

}
