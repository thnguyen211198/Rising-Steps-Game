using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDisplayed : MonoBehaviour {
    [SerializeField] GameObject entity;
    [SerializeField] int amount;

    public GameObject Entity { get => entity; set => entity = value; }
    public int Amount { get => amount; set => amount = value; }
    public EntityDisplayed(GameObject c, int a) {
        entity = c;
        amount = a;
    }    
}
