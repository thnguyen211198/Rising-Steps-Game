using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCostConfig : MonoBehaviour {
    public static int house1GoldCost = 20;
    public static int house1WoodsCost = 100;
    public static int house2GoldCost = 30;
    public static int house2WoodsCost = 120;
    public static int house3GoldCost = 50;
    public static int house3WoodsCost = 200;
    public static int house4GoldCost = 100;
    public static int house4WoodsCost = 400;
    public static int house5GoldCost = 120;
    public static int house5WoodsCost = 800;

    public static int tower1GoldCost = 80;
    public static int tower1WoodsCost = 300;
    public static int tower2GoldCost = 150;
    public static int tower2WoodsCost = 250;
    public static int tower3GoldCost = 400;
    public static int tower3WoodsCost = 400;

    public static int castleGoldCost = 500;
    public static int castleWoodsCost = 500;

    public static int pavementGoldCost = 10;
    public static int pavementWoodsCost = 10;

    public static int tavernGoldCost = 200;
    public static int tavernWoodsCost = 150;
    public static int farmGoldCost = 150;
    public static int farmWoodsCost = 200;

    public static void GetCost(string name, out int gold, out int woods) {
        switch (name) {
            case "House 1":
                gold = BuildingCostConfig.house1GoldCost;
                woods = BuildingCostConfig.house1WoodsCost;
                break;
            case "House 2":
                gold = BuildingCostConfig.house2GoldCost;
                woods = BuildingCostConfig.house2WoodsCost;
                break;
            case "House 3":
                gold = BuildingCostConfig.house3GoldCost;
                woods = BuildingCostConfig.house3WoodsCost;
                break;
            case "House 4":
                gold = BuildingCostConfig.house4GoldCost;
                woods = BuildingCostConfig.house4WoodsCost;
                break;
            case "House 5":
                gold = BuildingCostConfig.house5GoldCost;
                woods = BuildingCostConfig.house5WoodsCost;
                break;
            case "Tavern":
                gold = BuildingCostConfig.tavernGoldCost;
                woods = BuildingCostConfig.tavernWoodsCost;
                break;
            case "Farm":
                gold = BuildingCostConfig.farmGoldCost;
                woods = BuildingCostConfig.farmWoodsCost;
                break;
            case "Tower 1":
                gold = BuildingCostConfig.tower1GoldCost;
                woods = BuildingCostConfig.tower1WoodsCost;
                break;
            case "Tower 2":
                gold = BuildingCostConfig.tower2GoldCost;
                woods = BuildingCostConfig.tower2WoodsCost;
                break;
            case "Tower 3":
                gold = BuildingCostConfig.tower3GoldCost;
                woods = BuildingCostConfig.tower3WoodsCost;
                break;
            case "Castle":
                gold = BuildingCostConfig.castleGoldCost;
                woods = BuildingCostConfig.castleWoodsCost;
                break;
            case "Pavement":
                gold = BuildingCostConfig.pavementGoldCost;
                woods = BuildingCostConfig.pavementWoodsCost;
                break;
            default:
                gold = woods = 0;
                break;
        }
    }
}
