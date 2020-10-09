using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    [SerializeField] GameObject constructionArea;
    [SerializeField] GameObject house1;
    [SerializeField] GameObject house2;
    [SerializeField] GameObject house3;
    [SerializeField] GameObject house4;
    [SerializeField] GameObject house5;
    [SerializeField] GameObject tavern;
    [SerializeField] GameObject farm;    
    [SerializeField] GameObject tower1;
    [SerializeField] GameObject tower2;
    [SerializeField] GameObject tower3;
    [SerializeField] GameObject castle;
    [SerializeField] GameObject pavement;

    public GameObject ConstructionArea { get => constructionArea; }

    public void CreateSampleModel(GameObject model) {
        string buildingID = model.GetComponent<BuildingDragHandler>().Building.name;
        BuildingOnDisplay buildingChosen = BuildingUI.GetBuildingChosen(buildingID);
        PlayerBehavior player = FindObjectOfType<PlayerBehavior>();
        if (player.Gold >= buildingChosen.Gold && player.Woods >= buildingChosen.Woods) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Vector3 pos = hit.point;
                pos.y = 0;
                Instantiate(model, pos, Quaternion.identity);
            }            
        }
        else Debug.Log("Unaffordable");
    }

    public void LoadConstruction() {
        List<ConstructionData> constructions = FindObjectOfType<PlayerBehavior>().ConstructionArea;
        foreach (ConstructionData con in constructions) {
            Instantiate(constructionArea, con.building.Position, Quaternion.identity);
            constructionArea.GetComponent<ConstructionArea>().Construction = con;
        }        
    }
    public void LoadBuilding(string tag) {
        if (tag == "Player") LoadConstruction();
        List<BuildingData> BuildingOwned = GameObject.FindGameObjectWithTag(tag).GetComponent<PlayerBehavior>().BuildingOwned;
        foreach (BuildingData b in BuildingOwned) {
            GameObject building = GetBuilding(b.ID);
            if (building != null) Instantiate(building, b.Position, building.transform.rotation);
        }
    }
    public GameObject GetBuilding(string n) {
        GameObject g;
        switch (n) {
            case "House 1":
                g = house1;
                break;
            case "House 2":
                g = house2;
                break;
            case "House 3":
                g = house3;
                break;
            case "House 4":
                g = house4;
                break;
            case "House 5":
                g = house5;
                break;
            case "Tavern":
                g = tavern;
                break;
            case "Farm":
                g = farm;
                break;
            case "Tower 1":
                g = tower1;
                break;
            case "Tower 2":
                g = tower2;
                break;
            case "Tower 3":
                g = tower3;
                break;
            case "Castle":
                g = castle;
                break;
            case "Pavement":
                g = pavement;
                break;
            default:
                g = null;
                break;
        }
        return g;
    }
}
