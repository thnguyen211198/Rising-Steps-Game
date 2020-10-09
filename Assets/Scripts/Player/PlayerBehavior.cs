using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour {
    [SerializeField] PlayerData playerData;
    public PlayerData PlayerData => playerData;
    public string Username => playerData.Username;
    public string Password => playerData.Password;
    public int Exp => playerData.Exp;
    public List<BuildingData> BuildingOwned => playerData.BuildingOwned;
    public List<ConstructionData> ConstructionArea => playerData.ConstructionArea;
    public int Gold => playerData.Gold;
    public int Woods => playerData.Woods;
    public List<TroopsData> Troops => playerData.Troops;

    PlayerSaveManager playerSaveManager;

    public UnityEvent OnPlayerInit = new UnityEvent();
    public UnityEvent OnPlayerUpdated = new UnityEvent();

    public void InitializePlayer(string prefs) {
        playerSaveManager = GetComponent<PlayerSaveManager>();
        playerSaveManager.PlayerKey = PlayerPrefs.GetString(prefs);
        StartCoroutine(InitializePlayer());
    }
    IEnumerator InitializePlayer() {        
        var playerDataTask = playerSaveManager.LoadPlayer();
        yield return new WaitUntil(predicate: () => playerDataTask.IsCompleted);
        var playerData = playerDataTask.Result;
        UpdatePlayer(playerData);
    }
    
    public void ConstructNewBuilding(ConstructionData newConstruction){        
        ConstructionArea.Add(newConstruction);
        OnPlayerUpdated.Invoke();
    }
    public void ConstructNewBuilding(BuildingData newBuilding) {
        BuildingOwned.Add(newBuilding);
        OnPlayerUpdated.Invoke();
    }
    public void FinishConstruction(ConstructionData newConstruction) {
        Debug.Log("Player built " + newConstruction.building.ID);
        ConstructionArea.Remove(newConstruction);
        BuildingOwned.Add(newConstruction.building);
        OnPlayerUpdated.Invoke();
    }
    public bool TrainNewTroops(TroopsData newTroop) {
        int gold;
        TrainingCostConfig.GetCost(newTroop.ID, out gold);
        if (Gold >= gold) {
            for (int i = 0; i < Troops.Count; i++) {
                if (Troops[i].ID == newTroop.ID) {
                    TroopsData t = new TroopsData();
                    t.ID = Troops[i].ID;
                    t.Amount = Troops[i].Amount + newTroop.Amount;
                    Troops.RemoveAt(i);
                    Troops.Insert(i, t);
                    playerData.Gold -= gold;
                    OnPlayerUpdated.Invoke();                    
                    return true;
                }
            }
            Troops.Add(newTroop);
            playerData.Gold -= gold;
            OnPlayerUpdated.Invoke();
            return true;
        }
        else return false;
    }
    public void ReduceGold(int amount) {
        playerData.Gold -= amount;        
        OnPlayerUpdated.Invoke();
    }
    public void IncreaseGold(int amount) {
        playerData.Gold += amount;
        OnPlayerUpdated.Invoke();
    }
    public void ReduceWoods(int amount) {
        playerData.Woods -= amount;
        OnPlayerUpdated.Invoke();
    }
    public void IncreaseWoods(int amount) {
        playerData.Woods += amount;
        OnPlayerUpdated.Invoke();
    }
    public void UpdatePlayer(PlayerData newData) {        
        if (!newData.Equals(playerData)) {
            playerData = newData;
            OnPlayerInit.Invoke(); 
        }
    }
}
