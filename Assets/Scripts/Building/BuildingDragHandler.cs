using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingDragHandler : MonoBehaviour {
    [SerializeField] GameObject building;
    Button buildButton, cancelButton;
    Vector3 offset;
    float zCoord;
    bool collided;
    bool inScope;

    public GameObject Building { get => building; }

    void Start() {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        foreach (Button b in buttons) {
            if (b.name == "Build") buildButton = b;
            if (b.name == "Cancel") cancelButton = b;
        }
        buildButton.onClick.AddListener(CreateBuilding);
        cancelButton.onClick.AddListener(CancelBuilding);
        collided = false;
        inScope = false;
    }
    void Update() {
        Renderer[] renderers = transform.Find("Building").gameObject.GetComponentsInChildren<Renderer>();
        //Debug.Log("collided: " + collided + "; In scope: " + inScope);
        if (collided == false && inScope == true) {
            if (renderers != null) {
                foreach (Renderer r in renderers) r.material.color = Color.green;                
            }
            buildButton.interactable = true;            
        }
        else {
            if (renderers != null) {
                foreach (Renderer r in renderers) r.material.color = Color.red;                
            }
            buildButton.interactable = false;
        }
    }
    void OnMouseDown() {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }
    void OnMouseDrag() {
        float yCoord = transform.position.y;
        transform.position = GetMouseWorldPos() + offset;
        transform.position = new Vector3(transform.position.x, yCoord, transform.position.z);
    }
    Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Building" || other.tag == "Road") {
            collided = true;
        }
    }
    public void OnTriggerStay(Collider other) {
        if (other.tag == "Scope") {            
            inScope = true;
        }        
        if (other.tag == "Building" || other.tag == "Road") {
            collided = true;
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.tag == "Scope") {            
            inScope = false;
        }
        if (other.tag == "Building" || other.tag == "Road") {
            collided = false;
        }
    }
    public void CreateBuilding() {
        PlayerBehavior player = FindObjectOfType<PlayerBehavior>();
        BuildingController buildingController = FindObjectOfType<BuildingController>();
        if (building.tag == "Building") {
            GameObject c = Instantiate(buildingController.ConstructionArea, transform.position, Quaternion.identity);
            ConstructionArea constructionArea = c.GetComponent<ConstructionArea>();
            constructionArea.Construction = new ConstructionData();
            constructionArea.SetTime(System.DateTime.Now);
            constructionArea.SetBuilding(building.name, transform.position);
            player.ConstructNewBuilding(constructionArea.Construction);
            Destroy(gameObject);
        }
        else {
            Instantiate(building, transform.position, Quaternion.identity);
            BuildingData b = new BuildingData();
            b.ID = building.name;
            b.Position = transform.position;
            player.ConstructNewBuilding(b);
            FindObjectOfType<EditorPath>().InitRoad();
            MoveModelToNext();
        }
        int gold, woods;
        BuildingCostConfig.GetCost(Building.name, out gold, out woods);
        player.ReduceGold(gold);
        player.ReduceWoods(woods);
    }
    void MoveModelToNext() {
        Vector3 direction = new Vector3(0, 0, 0);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.tag == "Road") {
                Vector3 offset = transform.position - hitCollider.transform.position;
                Debug.Log(offset.x);
                Debug.Log(offset.z);
                if (Mathf.Abs(offset.x) > 2) direction.x = offset.x;
                else if (Mathf.Abs(offset.z) > 2) direction.z = offset.z;                   
            }
        }
        transform.Translate(direction);
    }
    public void CancelBuilding() {
        Destroy(gameObject);
    }
}
