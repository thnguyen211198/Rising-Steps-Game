using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnTroops : MonoBehaviour {
    [SerializeField] TroopsManager troopsManager;
    void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (troopsManager.ChosenSoldier != null) {
                Spawn();                
            }
        }
    }
    void Spawn() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag.Equals("Desert")) {
                if (troopsManager.Decrease()) {
                    Instantiate(troopsManager.ChosenSoldier, hit.point, Quaternion.identity);
                }                    
            }
        }
    }
}
