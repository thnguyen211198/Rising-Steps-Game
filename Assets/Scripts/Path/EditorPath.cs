using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour {
    [SerializeField] float connectDistance = 4f;
    Color rayColor = Color.white;
    List<List<PathObject>> paths;
    [SerializeField] List<PathObject> pathObjects;
    [SerializeField] List<PathObject> doors;

    public List<PathObject> PathObjects => pathObjects;
    public List<PathObject> Doors => doors;

    public List<List<PathObject>> Paths { get => paths; }

    public void InitRoad() {
        paths = new List<List<PathObject>>();
        pathObjects = new List<PathObject>(FindObjectsOfType<PathObject>());
        doors = new List<PathObject>();
        foreach (PathObject pathObject in pathObjects) {
            if (pathObject.tag.Equals("Door")) {
                pathObject.IsDoor = true;
                doors.Add(pathObject);
            }
            else {
                pathObject.IsDoor = false;
            }
            pathObject.Vicinity = new List<PathObject>();
        }

        for (int i = 0; i < pathObjects.Count; i++) {
            Vector3 position = pathObjects[i].transform.position;
            for (int j = i + 1; j < pathObjects.Count; j++) {
                float distance = Vector3.Distance(position, pathObjects[j].transform.position);
                if (distance < connectDistance) {
                    pathObjects[i].SetVicinity(pathObjects[j]);
                    pathObjects[j].SetVicinity(pathObjects[i]);
                }
            }
        }

        HashSet<PathObject> checkedPaths = new HashSet<PathObject>();
        foreach (PathObject p in pathObjects) {
            if (!checkedPaths.Contains(p)) {
                HashSet<PathObject> newSet = SearchEngine.GetPathConnection(p);
                checkedPaths.UnionWith(newSet);
                List<PathObject> path = new List<PathObject>(newSet);
                Paths.Add(path);
                foreach(PathObject child in path) child.ParentPath = path;                
            }
        }
    }
    //void OnDrawGizmos() {
    //    if (paths == null) paths = new List<List<PathObject>>();
    //    else paths.Clear();
    //    pathObjects = new List<PathObject>(FindObjectsOfType<PathObject>());
    //    doors = new List<PathObject>(FindObjectsOfType<PathObject>());

    //    Gizmos.color = rayColor;
    //    PathObject[] arr = FindObjectsOfType<PathObject>();
    //    pathObjects.Clear();
    //    doors.Clear();

    //    for (int i = 0; i < pathObjects.Count; i++) {
    //        Vector3 position = pathObjects[i].transform.position;            
    //        Gizmos.DrawWireSphere(position, 0.3f);            
    //        for (int j = i + 1; j < pathObjects.Count; j++) {                
    //            float distance = Vector3.Distance(position, pathObjects[j].transform.position);
    //            if (distance < connectDistance) {                    
    //                Gizmos.DrawLine(position, pathObjects[j].transform.position);
    //            }                
    //        }
    //    }
    //}
}
