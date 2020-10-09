using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionTimeConfig : MonoBehaviour {
    public static TimeSpan timeConstructHouse1 = new TimeSpan(0, 0, 10);
    public static TimeSpan timeConstructHouse2 = new TimeSpan(0, 0, 30);
    public static TimeSpan timeConstructHouse3 = new TimeSpan(0, 0, 20);
    public static TimeSpan timeConstructHouse4 = new TimeSpan(0, 0, 20);
    public static TimeSpan timeConstructHouse5 = new TimeSpan(0, 0, 20);

    public static TimeSpan timeConstructTavern = new TimeSpan(0, 0, 20);
    public static TimeSpan timeConstructFarm = new TimeSpan(0, 0, 20);

    public static TimeSpan timeConstructTower1 = new TimeSpan(0, 0, 20);
    public static TimeSpan timeConstructTower2 = new TimeSpan(0, 0, 20);
    public static TimeSpan timeConstructTower3 = new TimeSpan(0, 0, 20);

    public static TimeSpan timeConstructCastle = new TimeSpan(0, 0, 25);
    public static void GetTime(string name, out TimeSpan time) {
        switch (name) {
            case "House 1":
                time = timeConstructHouse1;
                break;
            case "House 2":
                time = timeConstructHouse2;
                break;
            case "House 3":
                time = timeConstructHouse3;
                break;
            case "House 4":
                time = timeConstructHouse4;
                break;
            case "House 5":
                time = timeConstructHouse5;
                break;
            case "Tavern":
                time = timeConstructTavern;
                break;
            case "Farm":
                time = timeConstructFarm;
                break;
            case "Tower 1":
                time = timeConstructTower1;
                break;
            case "Tower 2":
                time = timeConstructTower2;
                break;
            case "Tower 3":
                time = timeConstructTower3;
                break;
            case "Castle":
                time = timeConstructCastle;
                break;
            default:
                time = TimeSpan.Zero;
                break;
        }
    }
}
