using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using Firebase;
using UnityEngine.Events;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour {
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    [SerializeField] UnityEngine.UI.Text errorMessage;
    void Start() {
        CheckAndFix();
    }
    void CheckAndFix() {        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(continuation: task => {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            OnFirebaseInitialized.Invoke();
            if (task.Exception != null) {                
                errorMessage.gameObject.SetActive(true);
                errorMessage.text = "Failed to initialize Firebase with" + task.Exception.Message;
                //Debug.LogError(message: $"Failed to initialize Firebase with {task.Exception}");                
            }            
        });
    }
}
