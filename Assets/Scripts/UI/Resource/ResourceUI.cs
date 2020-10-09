using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {
    Transform goldTransform;
    Transform woodsTransform;
    void Start() {
        goldTransform = transform.Find("Resource").Find("Gold");
        woodsTransform = transform.Find("Resource").Find("Woods");
    }
    public void GetUserData() {
        //Debug.Log("Getting player's data");
        string username = PlayerPrefs.GetString("PLAYER_KEY");
        transform.Find("Player Info").Find("Username").GetComponent<Text>().text = username;
        GetGoldData();
        GetWoodsData();
    }
    void GetGoldData() {
        Text goldText = goldTransform.Find("Amount").GetComponent<Text>();
        int gold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().Gold;
        goldText.text = gold.ToString();
        BarUI goldBar = goldTransform.GetComponent<BarUI>();
        float scale = gold / (float)ResourceConfig.maxGold;
        goldBar.SetSize(scale);
    }
    void GetWoodsData() {
        Text woodsText = woodsTransform.Find("Amount").GetComponent<Text>();
        int woods = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().Woods;
        woodsText.text = woods.ToString();
        BarUI woodsBar = woodsTransform.GetComponent<BarUI>();
        float scale = woods / (float)ResourceConfig.maxWoods;
        woodsBar.SetSize(scale);
    }
}
