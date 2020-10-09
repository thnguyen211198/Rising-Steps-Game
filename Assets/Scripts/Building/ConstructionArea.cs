using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionArea : MonoBehaviour {
    [SerializeField] ConstructionData construction;

    string timeFormat = "dd/MM/yyyy hh:mm:ss tt";
    TimeSpan duration;
    [SerializeField] TimeSpan remain;
    Coroutine setCircleBarCoroutine;

    public ConstructionData Construction { get => construction; set => construction = value; }
    void Update() {
        UpdateDuration();
        UpdateRemain();
        if (setCircleBarCoroutine == null) setCircleBarCoroutine = StartCoroutine(SetCircleBar());
        CheckCompletion();
    }
    public void SetTime(DateTime t) {        
        construction.time = t.ToString(timeFormat);
    }
    public void SetBuilding(string id, Vector3 position) {
        construction.building = new BuildingData(id, position);        
    }
    void UpdateDuration() {
        DateTime constructionTime = DateTime.ParseExact(construction.time, timeFormat, CultureInfo.InvariantCulture);
        duration = DateTime.Now.Subtract(constructionTime);
    }    
    void UpdateRemain() {
        TimeSpan finishTime;
        ConstructionTimeConfig.GetTime(construction.building.ID, out finishTime);
        remain = finishTime.Subtract(duration);
    }
    IEnumerator SetCircleBar() {
        float scale = (float)(duration.TotalSeconds / (duration.TotalSeconds + remain.TotalSeconds));
        GetComponentInChildren<CircleBarUI>().SetSize(scale);
        yield return new WaitForSeconds(0.3f);
        setCircleBarCoroutine = null;
    }
    void CheckCompletion() {
        if (remain <= TimeSpan.Zero) {
            FindObjectOfType<PlayerBehavior>().FinishConstruction(construction);
            GameObject building = FindObjectOfType<BuildingController>().GetBuilding(construction.building.ID);
            Instantiate(building, transform.position, building.transform.rotation);
            Destroy(gameObject);
        }
        else {
            GetComponentInChildren<Text>().text = FormatTime(remain);
        }
    }                   
    string FormatTime(TimeSpan time) {
        string timeDisplayed;
        string days = time.ToString("%d") + "d";
        string hours = time.ToString("%h") + "h";
        string minutes = time.ToString("%m") + "m";
        string seconds = time.ToString("%s") + "s";
        if (days == "0d") {
            if (hours == "0h") {
                if (minutes == "0m") timeDisplayed = seconds;
                else timeDisplayed = minutes + seconds;
            }
            else timeDisplayed = hours + minutes;
        }
        else timeDisplayed = days + hours;
        return timeDisplayed;
    }
}
