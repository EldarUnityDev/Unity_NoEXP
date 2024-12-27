using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{

    public GameObject alertPipPrefab;

    public List<Image> alertPips = new List<Image>();

    public int alertLevel;
    public int maxAlertLevel;

    public Sprite emptyPip;
    public Sprite filledPip;

    public AudioSource alarmSound;

    private void Awake()
    {
        References.alarmManager = this;
        alarmSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(AlarmHasSounded() && alarmSound.isPlaying == false)
        {
            alarmSound.Play();
        }
        if(AlarmHasSounded() == false && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }
    }
    public void StopTheAlarm()
    {
        alertLevel = 0;
        UpdatePips();
    }
    public void SetUpLevel(int desiredMaxAlertLevel)
    {
        for(int i = 0; i < alertPips.Count; i++)
        {
            Destroy(alertPips[i].gameObject);
        }
        alertPips.Clear(); //wipes the list
        maxAlertLevel = desiredMaxAlertLevel;
        //for each alret level, create a pip, and store them in a list
        for (int i = 0; i < desiredMaxAlertLevel; i++)
        {
            GameObject newPip = Instantiate(alertPipPrefab, transform);
            alertPips.Add(newPip.GetComponent<Image>());
        }
        alertPips.Reverse(); //чтобы заполнялись снизу вверх
        
    }

    public void RaiseAlertLevel()
    {
        alertLevel++;
        UpdatePips();
    }
    
    public void SoundTheAlarm()
    {
        alertLevel = maxAlertLevel;
        UpdatePips();
    }

    public bool AlarmHasSounded()
    {
        if(maxAlertLevel == 0) { return false; } //чтобы на старте не пищало, пока ещё не всё подгрузилось
        return alertLevel >= maxAlertLevel;
    }

    void UpdatePips()
    {
        for(int i = 0;i < alertPips.Count; i++)
        {
            if(i < alertLevel)
            {
                alertPips[i].sprite = filledPip;
            } else
            {
                alertPips[i].sprite = emptyPip;
            }
        }
    }

}
