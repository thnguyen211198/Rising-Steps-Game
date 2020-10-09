using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour {
    [SerializeField] GameObject soldier;
    int warrior = 0;
    int wizard = 0;
    int knight = 0;
    AttackingUI ui;

    public GameObject ChosenSoldier {
        get { return soldier; }
        set { soldier = value; }
    }
    public int Warrior {
        get { return warrior; }
        set { warrior = value; }
    }
    public int Wizard {
        get { return wizard; }
        set { wizard = value; }
    }
    public int Knight {
        get { return knight; }
        set { knight = value; }
    }

    void Start() {
        ui = GetComponent<AttackingUI>();        
    }
    public void GetPlayerTroops() {
        PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        List<TroopsData> playerTroops = player.Troops;
        foreach (TroopsData t in playerTroops) {
            switch (t.ID) {
                case "Warrior":
                    warrior = t.Amount;
                    break;
                case "Wizard":
                    wizard = t.Amount;
                    break;
                case "Knight":
                    knight = t.Amount;
                    break;
                default:
                    Debug.Log("Undefinded Troops");
                    break;
            }
        }
        GetComponent<AttackingUI>().GetTroopAmount();
    }
    public bool Decrease() {
        if (ChosenSoldier.GetComponent<Warrior>() != null) {
            if (warrior > 0) {
                warrior--;
                ui.GetTroopAmount();
                return true;
            }
            else return false;
        }
        else if (ChosenSoldier.GetComponent<Wizard>() != null) {
            if (wizard > 0) {
                wizard--;
                ui.GetTroopAmount();
                return true;
            }
            else return false;
        }
        else if (ChosenSoldier.GetComponent<Knight>() != null) {
            if (knight > 0) {
                knight--;
                ui.GetTroopAmount();
                return true;
            }
            else return false;
        }
        return false;
    }
}
