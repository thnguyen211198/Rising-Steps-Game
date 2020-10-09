using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour {
    BuildingController buildingController;
    void Start() {
        buildingController = FindObjectOfType<BuildingController>();
    }
    public void OpenConstructionMenu() {
        Transform conMenu = transform.Find("Construction Menu");
        if (conMenu != null) conMenu.gameObject.SetActive(true);
        Transform conBtn = transform.Find("Construction Button");
        if (conBtn != null) conBtn.gameObject.SetActive(false);
        Transform batBtn = transform.Find("Battle Button");
        if (batBtn != null) batBtn.gameObject.SetActive(false);
    }
    public void CloseConstructionMenu() {        
        Transform conMenu = transform.Find("Construction Menu");
        if (conMenu != null) conMenu.gameObject.SetActive(false);
        Transform conBtn = transform.Find("Construction Button");
        if (conBtn != null) conBtn.gameObject.SetActive(true);
        Transform batBtn = transform.Find("Battle Button");
        if (batBtn != null) batBtn.gameObject.SetActive(true);
    }
    public void OpenBattleMenu() {
        Transform bat = transform.Find("Battle Menu");
        if (bat != null) bat.gameObject.SetActive(true);
        Transform conBtn = transform.Find("Construction Button");
        if (conBtn != null) conBtn.gameObject.SetActive(false);
        Transform batBtn = transform.Find("Battle Button");
        if (batBtn != null) batBtn.gameObject.SetActive(false);
    }
    public void CloseBattleMenu() {
        Transform bat = transform.Find("Battle Menu");
        if (bat != null) bat.gameObject.SetActive(false);
        Transform conBtn = transform.Find("Construction Button");
        if (conBtn != null) conBtn.gameObject.SetActive(true);
        Transform batBtn = transform.Find("Battle Button");
        if (batBtn != null) batBtn.gameObject.SetActive(true);
    }
    public void ChooseBuilding(GameObject model) {
        buildingController.CreateSampleModel(model);
        CloseConstructionMenu();
    }
    public static BuildingOnDisplay GetBuildingChosen(string buildingID) {
        BuildingOnDisplay [] buildingsOnDisplay = FindObjectsOfType<BuildingOnDisplay>();
        foreach (BuildingOnDisplay b in buildingsOnDisplay) {
            if (b.name == buildingID) return b;            
        }
        return null;        
    }
    public void FindOpponent() {
        SceneManager.LoadScene("looking for opponent");
    }

    public void GoChallenge1() {
        SceneManager.LoadScene("scene 2");
    }
}
