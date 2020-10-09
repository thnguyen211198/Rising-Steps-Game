using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCostConfig : MonoBehaviour {
    public static int warriorGoldCost = 20;
    public static int wizardGoldCost = 30;
    public static int knightGoldCost = 35;

    public static void GetCost(string name, out int gold) {
        switch (name) {
            case "Warrior":
                gold = warriorGoldCost;                
                break;
            case "Wizard":
                gold = wizardGoldCost;
                break;
            case "Knight":
                gold = knightGoldCost;
                break;
            default:
                gold = 0;
                break;
        }
    }
}
