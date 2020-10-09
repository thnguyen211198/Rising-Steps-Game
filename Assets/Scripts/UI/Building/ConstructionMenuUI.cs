using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenuUI : MonoBehaviour {
    readonly string[] iconName = { "House Icon", "Economy Icon", "Defense Tower Icon", "Barrack Icon", "Decoration Icon" };
    readonly string[] tabsName = { "House Tab", "Economy Tab", "Defense Tower Tab", "Barrack Tab", "Decoration Tab" };
    void Start() {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button b in buttons) {  
            for (int i = 0; i < iconName.Length; i++) {                
                if (b.name == iconName[i]) {                    
                    int index = i;
                    b.onClick.AddListener(() => {                                                
                        TurnTab(tabsName[index]);                        
                    });
                }
            }
        }
    }
    void TurnTab(string tabName) {
        foreach (string t in tabsName) {
            GameObject tab = transform.Find(t).gameObject;
            if (t == tabName) tab.SetActive(true);
            else tab.SetActive(false);
        }        
    }
}
