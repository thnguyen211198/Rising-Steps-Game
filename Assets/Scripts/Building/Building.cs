using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour, IDamageable {
    [SerializeField] string buildingName;
    [SerializeField] int hitPoints = 5000;
    [SerializeField] GameObject smoke;
    BarUI healthBar;

    public string BuildingName { get => buildingName; set => buildingName = value; }
    public int HitPoints { get => hitPoints; }

    void Start() {
        GetAttribute();
    }
    protected void GetAttribute() {
        healthBar = transform.Find("HealthBar").gameObject.GetComponent<BarUI>();
        healthBar.gameObject.SetActive(false);
    }
    public void GetHit(int damage) {
        healthBar.gameObject.SetActive(true);
        hitPoints -= damage;        
        if (HitPoints > 0) {
            float scale = HitPoints / 5000f;
            healthBar.SetSize(scale);
        }
        else {
            Destroy(gameObject);
            Explode();
        }
    }
    void Explode() {
        Instantiate(smoke, transform.position, Quaternion.identity);
    }
}
