using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEngine : MonoBehaviour {
    struct elementTraversed {
        public elementTraversed(PathObject p) {
            pathObject = p;
            path = new List<PathObject>();
        }
        public PathObject pathObject;
        public List<PathObject> path;
    }

    public static HashSet<PathObject> GetPathConnection(PathObject from) {
        HashSet<PathObject> setChecked = new HashSet<PathObject>();
        setChecked.Add(from);
        Stack<PathObject> s = new Stack<PathObject>();
        foreach (PathObject p in from.Vicinity) s.Push(p);
        while (s.Count > 0) {
            PathObject objTraversed = s.Pop();
            setChecked.Add(objTraversed);
            foreach (PathObject p in objTraversed.Vicinity) {
                if (!setChecked.Contains(p)) {
                    s.Push(p);                    
                }
            }
            if (s.Count > 9999) break;
        }
        return setChecked;
    }

    public static List<PathObject> DepthFirstSearch(PathObject from, PathObject destination) {        
        HashSet<PathObject> setPositionChecked = new HashSet<PathObject>();
        setPositionChecked.Add(from);
        Stack<elementTraversed> s = new Stack<elementTraversed>();
        foreach (PathObject p in from.Vicinity) {
            elementTraversed e = new elementTraversed(p);
            e.path.Add(from);
            e.path.Add(p);
            s.Push(e);
        }
        while (s.Count > 0) {
            elementTraversed e = s.Pop();
            setPositionChecked.Add(e.pathObject);
            List<PathObject> traversed = e.path;
            if (IsGoal(e.pathObject, destination)) {
                return traversed;
            }
            foreach (PathObject p in e.pathObject.Vicinity) {
                if (!setPositionChecked.Contains(p)) {
                    elementTraversed successor = new elementTraversed(p);
                    successor.path.AddRange(traversed);
                    successor.path.Add(p);
                    s.Push(successor);
                }
            }
            if (s.Count > 9999) break;
        }
        return null;
    }

    public static PathObject GetNearestPathObject(Vector3 from, List<PathObject> paths) {
        PathObject pathObject = paths[0];
        float minDistance = Vector3.Distance(from, pathObject.transform.position);
        foreach (PathObject p in paths) {
            float distance = Vector3.Distance(from, p.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                pathObject = p;
            }
        }
        return pathObject;
    }

    static bool IsGoal(PathObject p, PathObject goal) {
        if (p == goal) return true;
        return false;
    }
}
