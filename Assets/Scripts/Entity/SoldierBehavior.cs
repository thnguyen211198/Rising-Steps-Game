using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierBehavior : MonoBehaviour, IDamageable {
    Animator animator;
    protected NavMeshAgent agent;
    protected Coroutine setStatusCoroutine;
    protected bool isTouchTarget;
    protected IAttackBehavior attackBehavior;

    [SerializeField] GameObject objective;
    [SerializeField] protected int damage;
    [SerializeField] protected int healthPoint;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;

    [SerializeField] Sprite avatar;
    public Sprite Avatar { get => avatar; }

    protected void GetAttribute() {
        animator = GetComponentInChildren<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1;
    }
    protected IEnumerator Act() {
        if (objective != null) {
            float distance = Vector3.Distance(transform.position, objective.transform.position);
            if (distance <= attackRange || isTouchTarget == true) {
                Idle();
                Attack(objective);
            }
            else Move();
        }
        yield return new WaitForSeconds(attackSpeed);
        setStatusCoroutine = null;
    }
    protected void SetTarget() {
        CannonTower []cannons = FindObjectsOfType<CannonTower>();
        MageTower[] mages = FindObjectsOfType<MageTower>();
        Guardian [] guardians = FindObjectsOfType<Guardian>();
        List<GameObject> attackObjs = new List<GameObject>();
        foreach(CannonTower c in cannons) attackObjs.Add(c.gameObject);
        foreach (MageTower m in mages) attackObjs.Add(m.gameObject);
        foreach (Guardian g in guardians) attackObjs.Add(g.gameObject);
        List<GameObject> objectives = new List<GameObject>(GameObject.FindGameObjectsWithTag("Building"));

        GameObject obj;
        if (attackObjs.Count > 0) obj = GetNearestEnemy(attackObjs);
        else obj = GetNearestEnemy(objectives);
        if (objective != obj) {
            objective = obj;
            isTouchTarget = false;
        }
        if (objective != null) {
            agent.SetDestination(objective.transform.position);            
        }
    }

    GameObject GetNearestEnemy(List<GameObject> enemies) {
        if (enemies.Count == 0) return null;
        GameObject nearestEnemy = enemies[0];
        float minDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == objective) isTouchTarget = true;
    }

    void Move() {
        agent.isStopped = false;
        animator.SetBool("Moving", true);
    }
    void Idle() {
        agent.isStopped = true;
        animator.SetBool("Moving", false);
    }
    void Attack(GameObject objective) {
        if (attackBehavior.Attack(objective, damage)) {
            animator.SetTrigger("Attacking");
        }
    }
    public void GetHit(int damage) {
        healthPoint -= damage;
        if (healthPoint > 0) {
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Hit() {
    }
    public void FootR() {
    }
    public void FootL() {
    }
}
