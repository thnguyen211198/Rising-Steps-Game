using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneForPlayerSaveState : MonoBehaviour {
    [SerializeField] PlayerSaveManager playerSaveManager;
    [SerializeField] string sceneForSaveExists;
    [SerializeField] string sceneForNoSave;
    Coroutine coroutine;
    public void Trigger() {
        if (coroutine == null){
            coroutine = StartCoroutine(LoadSceneCoroutine());
        }
    }
    IEnumerator LoadSceneCoroutine(){
        var saveExistsTask = playerSaveManager.SaveExists();
        yield return new WaitUntil(predicate: () => saveExistsTask.IsCompleted);
        if (saveExistsTask.Result){
            SceneManager.LoadScene(sceneForSaveExists);
        }
        else {
            SceneManager.LoadScene(sceneForNoSave);
        }
        coroutine = null;
    }
}
