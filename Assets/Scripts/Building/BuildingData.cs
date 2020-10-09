using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuildingData{
    public string ID;
    public Vector3 Position;    
    public BuildingData(string id, Vector3 pos) {
        ID = id;
        Position = pos;
    }
}
