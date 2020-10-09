using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingMenu : MonoBehaviour {
    [SerializeField] List<CharacterDisplayed> characterDisplayeds;
    string chosenCharacter;

    void Start() {
        ShowCharacter("Warrior");
    }

    public void ShowCharacter(string id) {
        foreach (CharacterDisplayed c in characterDisplayeds) {
            if (id == c.Name) {                
                Transform info = transform.Find("Information");
                info.Find("Avatar").GetComponent<Image>().sprite = c.Avatar;
                info.Find("Character Name").GetComponent<Text>().text = c.Name;
                foreach(BarUI bar in GetComponentsInChildren<BarUI>()) {
                    if (bar.gameObject.name == "HP") bar.SetSize(c.HPPoint);
                    if (bar.gameObject.name == "Damage") bar.SetSize(c.DamagePoint);
                    if (bar.gameObject.name == "Armor") bar.SetSize(c.ArmorPoint);
                }                
                chosenCharacter = c.Name;
            }
        }
    }
    public void Train() {
        if (chosenCharacter != null) {
            PlayerBehavior player = FindObjectOfType<PlayerBehavior>();
            int total = 0;
            foreach (TroopsData troop in player.Troops) {
                total += troop.Amount;
            }
            if (total < FindObjectOfType<Castle>().Capacity) {
                TroopsData t = new TroopsData();
                t.ID = chosenCharacter;
                t.Amount = 1;
                FindObjectOfType<PlayerBehavior>().TrainNewTroops(t);
            }
            else {
                Debug.Log("Castle is overload, cannot trains more troops");
            }
        }
    }
}
