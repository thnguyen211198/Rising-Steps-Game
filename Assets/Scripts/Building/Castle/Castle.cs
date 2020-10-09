using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Castle : Building {
    [SerializeField] GameObject warrior;
    [SerializeField] GameObject wizard;
    [SerializeField] GameObject knight;

    [SerializeField] int currentAmount;
    [SerializeField] int capacity;

    public int Capacity { get => capacity; }
    public int CurrentAmount { get => currentAmount; }

    void Start() {
        GetAttribute();
    }
    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject() && SceneManager.GetActiveScene().name == "scene player") {
            TrainingUI trainingUI = FindObjectOfType<TrainingUI>();
            trainingUI.OpenTrainingForm(this);
        }
    }
    public void Train(string character) {
        Debug.Log("training");
        PlayerBehavior player = FindObjectOfType<PlayerBehavior>();
        TroopsData t = new TroopsData();
        switch (character) {
            case "Warrior":                                
                t.ID = "Warrior";
                t.Amount = 1;
                player.TrainNewTroops(t);
                break;
            case "Wizard":
                t.ID = "Wizard";
                t.Amount = 1;
                player.TrainNewTroops(t);
                break;
            case "Knight":
                t.ID = "Knight";
                t.Amount = 1;
                player.TrainNewTroops(t);
                break;
            default:
                Debug.Log("Undefined troops");
                break;
        }
    }
    public GameObject GetTroop(string id) {
        GameObject g;
        switch (id) {
            case "Warrior":
                g = warrior;
                break;
            case "Wizard":
                g = wizard;
                break;
            case "Knight":
                g = knight;
                break;
            default:
                g = null;
                break;
        }
        return g;
    }
}
