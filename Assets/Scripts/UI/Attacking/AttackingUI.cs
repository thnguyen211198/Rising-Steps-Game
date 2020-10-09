using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttackingUI : MonoBehaviour {
    [SerializeField] Text opponent;
    [SerializeField] Text warriorAmount;
    [SerializeField] Text wizardAmount;
    [SerializeField] Text knightAmount;
    TroopsManager troopsManager;
    Coroutine checkBattleResultCoroutine;
    void Start() {
        troopsManager = GetComponent<TroopsManager>();
        opponent.text = PlayerPrefs.GetString("OPPONENT_KEY");
    }
    void Update() {
        if (checkBattleResultCoroutine == null) checkBattleResultCoroutine = StartCoroutine(CheckBattleResult());
    }
    public void Return() {
        SceneManager.LoadScene("scene player");
    }
    IEnumerator CheckBattleResult() {        
        yield return new WaitForSeconds(0.2f);
        if (FindObjectOfType<PlayerBehavior>().Username != "") {
            GameObject[] building = GameObject.FindGameObjectsWithTag("Building");
            if (building.Length < 1) {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("scene player");
            };
        }
        checkBattleResultCoroutine = null;
    }
    public void GetTroopAmount() {
        warriorAmount.text = "x" + troopsManager.Warrior;
        wizardAmount.text = "x" + troopsManager.Wizard;
        knightAmount.text = "x" + troopsManager.Knight;
    }
    public void ChooseSoldier(GameObject soldier) {
        troopsManager.ChosenSoldier = soldier;
    }
}
