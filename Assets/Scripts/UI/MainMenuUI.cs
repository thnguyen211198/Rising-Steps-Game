using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour{    
    public void ShowTextBox(){
        GameObject t = transform.Find("Intro").gameObject;
        t.SetActive(false);        
        GameObject usr = transform.Find("Username Input").gameObject;
        usr.SetActive(true);
        GameObject psw = transform.Find("Password Input").gameObject;
        psw.SetActive(true);
        GameObject loginBtn = transform.Find("Login Button").gameObject;
        loginBtn.SetActive(true);
        GameObject registerBtn = transform.Find("Register Button").gameObject;
        registerBtn.SetActive(true);
        Button b = transform.Find("Background").gameObject.GetComponent<Button>();
        b.interactable = false;
    }
    public void PrintInvalidPlayer(){
        GameObject message = transform.Find("Message").gameObject;
        message.SetActive(true);
        message.GetComponent<Text>().color = Color.red;
        message.GetComponent<Text>().text = "Invalid Account!";
    }
    public void RegisterFailed() {
        GameObject message = transform.Find("Message").gameObject;
        message.SetActive(true);
        message.GetComponent<Text>().color = Color.red;
        message.GetComponent<Text>().text = "Someone already has that username!";
    }
    public void RegisterSuccessfully() {
        GameObject message = transform.Find("Message").gameObject;
        message.SetActive(true);
        message.GetComponent<Text>().color = Color.green;
        message.GetComponent<Text>().text = "Registration completed successfully!";
    }

    public string GetUsername(){                
        return transform.Find("Username Input").Find("Username Text").gameObject.GetComponent<Text>().text;
    }
    public string GetPassword(){
        return transform.Find("Password Input").Find("Password Text").gameObject.GetComponent<Text>().text;
    }
}
