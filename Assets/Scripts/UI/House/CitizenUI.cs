using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitizenUI : MonoBehaviour {
    Transform buildingUI;
    Transform form;
    [SerializeField] Sprite house1;
    [SerializeField] Sprite house2;
    [SerializeField] Sprite house3;
    [SerializeField] Sprite house4;
    [SerializeField] Sprite house5;
    
    [SerializeField] GameObject entityPrefab;
    List<EntityDisplayed> citizenDisplayed;
    Vector3 lastPosition;
    int align = 65;
    void Start() {
        buildingUI = FindObjectOfType<BuildingUI>().transform;
        form = transform.Find("Form");
        citizenDisplayed = new List<EntityDisplayed>();        
    }
    public void OpenCitizenForm(House house) {
        buildingUI.gameObject.SetActive(false);
        form.gameObject.SetActive(true);
        Image houseImage = form.Find("Image").GetComponent<Image>();
        houseImage.sprite = ChooseHouseSprite(house.BuildingName);
        foreach(EntityDisplayed c in GetComponentsInChildren<EntityDisplayed>()) {
            Destroy(c.gameObject);
        }
        lastPosition = form.Find("Residents").position;
        citizenDisplayed.Clear();
        Group(house.Citizens);
        GetAmount();
        form.Find("Attribute").Find("Hitpoints").GetComponent<Text>().text = house.HitPoints.ToString();
    }
    Sprite ChooseHouseSprite(string name) {
        Sprite sprite;
        switch (name) {
            case "House 1":
                sprite = house1;
                break;
            case "House 2":
                sprite = house2;
                break;
            case "House 3":
                sprite = house3;
                break;
            case "House 4":
                sprite = house4;
                break;
            case "House 5":
                sprite = house5;
                break;
            default:
                sprite = null;
                break;
        }
        return sprite;
    }
    void Group(List<GameObject> citizens) {
        foreach (GameObject c in citizens) {
            int index = -1;
            for (int j = 0; j < citizenDisplayed.Count; j++) {
                if (c == citizenDisplayed[j].Entity) index = j;
            }
            if (index == -1) {                
                GameObject newElement = Instantiate(entityPrefab, lastPosition, Quaternion.identity);
                newElement.transform.SetParent(form.Find("Residents"));

                EntityDisplayed newCitizenClass = newElement.GetComponent<EntityDisplayed>();
                newCitizenClass.Entity = c;
                newCitizenClass.Amount = 1;
                newElement.GetComponentInChildren<Image>().sprite = newCitizenClass.Entity.GetComponent<Citizen>().Avatar;

                citizenDisplayed.Add(newCitizenClass);
                lastPosition.x += align;
            }
            else citizenDisplayed[index].Amount++;
        }        
    }
    void GetAmount() {
        foreach(EntityDisplayed c in citizenDisplayed) {
            c.gameObject.GetComponentInChildren<Text>().text = "x" + c.Amount.ToString();
        }
    }
    public void CloseCitizenForm() {
        buildingUI.gameObject.SetActive(true);
        form.gameObject.SetActive(false);
    }
}
