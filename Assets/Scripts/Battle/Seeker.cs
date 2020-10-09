using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seeker : MonoBehaviour {
    PlayerData playerData, opponentData;    
    Coroutine playerCoroutine, opponentCoroutine;

    void Start() {
        Debug.Log("Preparing...");
        playerCoroutine = StartCoroutine(GetPlayer());
        opponentCoroutine = StartCoroutine(GetOpponent());
    }
    void Update() {
        if (playerCoroutine == null && opponentCoroutine == null) {
            PlayerPrefs.SetString("OPPONENT_KEY", opponentData.Username);
            Debug.Log(playerData.Username + " vs " + opponentData.Username);            
            SceneManager.LoadScene("scene opponent");
        }
    }
    IEnumerator GetPlayer() {
        var playerDataTask = PlayerSaveManager.LoadPlayer(PlayerPrefs.GetString("PLAYER_KEY"));
        yield return new WaitUntil(predicate: () => playerDataTask.IsCompleted);
        playerData = playerDataTask.Result;
        playerCoroutine = null;
    }
    IEnumerator GetOpponent() {
        var opponentDataTask = PlayerSaveManager.LoadAllPlayers();
        yield return new WaitUntil(predicate: () => opponentDataTask.IsCompleted);
        int l = opponentDataTask.Result.Count;
        opponentData = opponentDataTask.Result[4];
        //do {
        //    int i = Random.Range(0, l);
        //    opponentData = opponentDataTask.Result[i];
        //} while (opponentData.Username == playerData.Username);
        opponentCoroutine = null;
    }
}
