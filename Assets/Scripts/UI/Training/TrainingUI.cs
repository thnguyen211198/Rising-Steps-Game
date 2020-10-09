using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingUI : MonoBehaviour {
    Transform buildingUI;
    Transform form;
    Transform trainingMenu;

    [SerializeField] GameObject entityPrefab;
    Vector3 lastPosition;
    int align = 65;
    void Start() {
        buildingUI = FindObjectOfType<BuildingUI>().transform;
        form = transform.Find("Form");
        trainingMenu = transform.Find("Training Menu");
    }
    public void OpenTrainingForm(Castle castle) {
        buildingUI.gameObject.SetActive(false);        
        form.gameObject.SetActive(true);
        form.Find("Attribute").Find("Hitpoints").GetComponent<Text>().text = castle.HitPoints.ToString();
        lastPosition = form.Find("Troops").position;
        foreach (EntityDisplayed c in GetComponentsInChildren<EntityDisplayed>()) {
            Destroy(c.gameObject);
        }
        SetTroopAmount(castle);
    }
    void SetTroopAmount(Castle castle) {
        PlayerBehavior player = FindObjectOfType<PlayerBehavior>();
        int total = 0;
        foreach(TroopsData t in player.Troops) {            
            GameObject newElement = Instantiate(entityPrefab, lastPosition, Quaternion.identity);
            newElement.transform.SetParent(form.Find("Troops"));
            EntityDisplayed newTroopClass = newElement.GetComponent<EntityDisplayed>();
            newTroopClass.Entity = castle.GetTroop(t.ID);
            newTroopClass.Amount = t.Amount;
            newElement.GetComponentInChildren<Image>().sprite = newTroopClass.Entity.GetComponent<SoldierBehavior>().Avatar;
            newElement.GetComponentInChildren<Text>().text = "x" + newTroopClass.Amount.ToString();
            total += t.Amount;
            lastPosition.x += align;
        }
        form.Find("Attribute").Find("Troop Capacity").GetComponent<Text>().text = total +" / " + castle.Capacity;
    }
    public void OpenTrainingMenu() {
        form.gameObject.SetActive(false);
        trainingMenu.gameObject.SetActive(true);
    }
    public void CloseTrainingMenu() {
        buildingUI.gameObject.SetActive(true);
        form.gameObject.SetActive(false);
        trainingMenu.gameObject.SetActive(false);
    }
}
