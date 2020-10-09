using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPlayerToSave : MonoBehaviour{    
    [SerializeField] PlayerSaveManager playerSaveManager;
    [SerializeField] PlayerBehavior player;
    void Reset(){
        playerSaveManager = GetComponent<PlayerSaveManager>();
        player = GetComponent<PlayerBehavior>();        
    }
    public void HandlePlayerUpdated(){
        playerSaveManager.SavePlayer(player.PlayerData);
    }
}
