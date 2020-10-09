using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObject : MonoBehaviour{
    List<PathObject> vicinity;
    bool isDoor;
    [SerializeField] List<PathObject> parentPath;

    public List<PathObject> Vicinity { get => vicinity; set => vicinity = value; }
    public bool IsDoor { get => isDoor; set => isDoor = value; }
    public List<PathObject> ParentPath { get => parentPath; set => parentPath = value; }

    public void SetVicinity(PathObject p) {
        if (Vicinity == null) Vicinity = new List<PathObject>();
        if (!Vicinity.Contains(p)) Vicinity.Add(p);        
    }
}
