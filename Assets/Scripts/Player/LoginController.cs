using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour{    
    [SerializeField] MainMenuUI mainMenuUI;
    PlayerData player;        
    Coroutine coroutine;
    PlayerSaveManager playerSaveManager;
    void Start(){
        playerSaveManager = GetComponent<PlayerSaveManager>();        
    }
    public void RegisterAccount(){
        if (coroutine == null) coroutine = StartCoroutine(RegisterAccountTask());        
    }
    public void Login(){
        if (coroutine == null) coroutine = StartCoroutine(LoginTask());        
    }
    public void Logout() {
        SceneManager.LoadScene("start scene");
    }
    public IEnumerator RegisterAccountTask(){
        GetDataUI();
        var checkExistenceTask = playerSaveManager.SaveExists();
        yield return new WaitUntil(predicate: () => checkExistenceTask.IsCompleted);
        if (!checkExistenceTask.Result){
            playerSaveManager.SavePlayer(player);
            FindObjectOfType<MainMenuUI>().RegisterSuccessfully();
        }
        else {
            FindObjectOfType<MainMenuUI>().RegisterFailed();
        }
        coroutine = null;
    }
    public IEnumerator LoginTask(){
        GetDataUI();

        var playerDataTask = playerSaveManager.LoadPlayer();
        yield return new WaitUntil(predicate: () => playerDataTask.IsCompleted);
        var playerData = playerDataTask.Result;
        if (playerData.Username != null && playerData.Password == player.Password) {            
            PlayerPrefs.SetString("PLAYER_KEY", player.Username);
            SceneManager.LoadScene("scene player");            
        }
        else{
            mainMenuUI.PrintInvalidPlayer();
        }
        coroutine = null;
    }
    void GetDataUI(){         
        player.Username = mainMenuUI.GetUsername();
        player.Password = mainMenuUI.GetPassword();
        player.BuildingOwned = new List<BuildingData>();
        player.Gold = ResourceConfig.startGold;
        player.Woods = ResourceConfig.startWoods;        
        playerSaveManager.PlayerKey = player.Username;        
    }
}
