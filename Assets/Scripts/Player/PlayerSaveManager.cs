using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour {
    string playerKey;
    public string PlayerKey{
        get {return playerKey;}
        set {playerKey = value;}
    }    
    public static async Task<List<PlayerData>> LoadAllPlayers() {
        var dataSnapshot = await FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync();
        List<PlayerData> players = new List<PlayerData>();
        foreach(DataSnapshot data in dataSnapshot.Children) {
            players.Add(JsonUtility.FromJson<PlayerData>(data.GetRawJsonValue()));
        }
        return players;
    }
    public static async Task<PlayerData> LoadPlayer(string k) {
        var dataSnapshot = await FirebaseDatabase.DefaultInstance.GetReference(path: k).GetValueAsync();
        if (!dataSnapshot.Exists) return new PlayerData();        
        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }
    public void SavePlayer(PlayerData player) {
        PlayerPrefs.SetString(playerKey, JsonUtility.ToJson(player));
        FirebaseDatabase.DefaultInstance.GetReference(path: playerKey).SetRawJsonValueAsync(JsonUtility.ToJson(player));
    }
    public async Task<PlayerData> LoadPlayer() {
        //Debug.Log(database);
        var dataSnapshot = await FirebaseDatabase.DefaultInstance.GetReference(path: playerKey).GetValueAsync();        
        if (!dataSnapshot.Exists) {
            return new PlayerData();
        }
        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }
    public async Task<bool> SaveExists() {
        var dataSnapshot = await FirebaseDatabase.DefaultInstance.GetReference(path: playerKey).GetValueAsync();
        return dataSnapshot.Exists;
    }
    public void EraseSave() {
        FirebaseDatabase.DefaultInstance.GetReference(path: playerKey).RemoveValueAsync();        
    }
}
