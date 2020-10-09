using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour{
    [SerializeField] Sprite avatar;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] List<PathObject> pathToFollow;

    EditorPath pathHolder;
    int currentWayPointID = 0;
    float reachDistance = 0.2f;    
    Animator animator;

    Coroutine idleCoroutine;

    public Sprite Avatar { get => avatar; }

    void Start() {
        animator = GetComponent<Animator>();
        pathHolder = FindObjectOfType<EditorPath>();
        speed = Random.Range(2, 4);
        MoveToNearestPathOject();
    }

    void Update() {
        MoveOnAPath();
        //if (GameObject.FindGameObjectWithTag("Enemy") != null) {
        //    GoInsideABuilding();
        //    speed = 5;
        //}
    }

    void MoveToNearestPathOject() {   
        if (pathHolder.PathObjects.Count > 0) {
            PathObject p = SearchEngine.GetNearestPathObject(transform.position, pathHolder.PathObjects);
            pathToFollow.Clear();
            pathToFollow.Add(p);
        }        
    }

    void MoveOnAPath() {
        if (pathToFollow != null) {
            animator.SetBool("Moving", true);
            Vector3 from = transform.position;
            Vector3 to = pathToFollow[currentWayPointID].transform.position;
            
            transform.position = Vector3.MoveTowards(from, to, speed * Time.deltaTime);            
            if (to - from != Vector3.zero) {
                var rotation = Quaternion.LookRotation(to - from);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }

            float distance = Vector3.Distance(from, to);
            if (distance <= reachDistance) {
                currentWayPointID++;
                //if (idleCoroutine == null) StartCoroutine(RandIdle());
            }            
            if (currentWayPointID >= pathToFollow.Count) {
                PathObject destination = pathToFollow[pathToFollow.Count - 1];
                if (destination.tag == "Door") Destroy(gameObject);
                else {
                    currentWayPointID = 0;
                    int opt = Random.Range(0, 1000);
                    if (opt < 125) GoInsideABuilding(destination);
                    else UpdatePath(destination);
                }
            }
        }
        else {
            pathToFollow = new List<PathObject>();
            currentWayPointID = 0;
            MoveToNearestPathOject();
        }
    }
    //IEnumerator RandIdle() {
    //    int randIdle = Random.Range(0, 1000);
    //    if (randIdle < 50) {
    //        idleDuration = Random.Range(5, 10);
    //        animator.SetBool("Moving", false);

    //    }
    //}
    void GoInsideABuilding(PathObject currentPos) {
        List<PathObject> doors = new List<PathObject>();
        foreach (PathObject p in currentPos.ParentPath) {
            if (p.tag == "Door") doors.Add(p);
        }
        if (doors.Count > 0) {
            int rand = Random.Range(0, doors.Count - 1);
            pathToFollow = SearchEngine.DepthFirstSearch(currentPos, doors[rand]);
        }
        else UpdatePath(currentPos);
    }
    void UpdatePath(PathObject currentPos) {
        int rand = Random.Range(0, currentPos.ParentPath.Count - 1);        
        PathObject destination = currentPos.ParentPath[rand];        
        if (!destination.IsDoor) pathToFollow = SearchEngine.DepthFirstSearch(currentPos, destination);
        else UpdatePath(currentPos);
    }    
}
