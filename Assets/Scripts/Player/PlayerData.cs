using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData {
    public string Username;
    public string Password;
    public int Exp;
    public List<BuildingData> BuildingOwned;
    public List<ConstructionData> ConstructionArea;
    public int Gold;
    public int Woods;
    public List<TroopsData> Troops;
    public BarrackData Barrack;
}
