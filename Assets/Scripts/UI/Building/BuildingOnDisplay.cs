using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOnDisplay : MonoBehaviour {    
    [SerializeField] Text goldText, woodsText;
    int gold, woods;    
    public int Gold => gold;
    public int Woods => woods;

    void Start() {        
        BuildingCostConfig.GetCost(name, out gold, out woods);
        goldText.text = gold.ToString();
        woodsText.text = woods.ToString();
    }    
}
