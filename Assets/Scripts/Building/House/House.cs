using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class House : Building {
    [SerializeField] List<GameObject> citizens;
    public List<GameObject> Citizens { get => citizens; set => citizens = value; }

    void Start() {
        GetAttribute();        
        GenerateCitizen();
    }

    void GenerateCitizen() {
        if (FindObjectsOfType<PathObject>().Length > 0 && SceneManager.GetActiveScene().name == "scene player") {
            Transform door = transform.Find("Door");
            foreach (GameObject c in Citizens) {
                Instantiate(c, door.position, Quaternion.identity);
            }
        }
    }

    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject() && SceneManager.GetActiveScene().name == "scene player") {
            CitizenUI citizenUI = FindObjectOfType<CitizenUI>();
            citizenUI.OpenCitizenForm(this);
        }
    }
}
