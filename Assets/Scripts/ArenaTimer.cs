using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaTimer : MonoBehaviour
{

    //public AudioSource alarmSound;

    public float timeLimit;
    float secondsLeft;
    public bool enemiesHaveSpawned;

    private void Awake()
    {
        References.arenaTimer = this;
        //alarmSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        secondsLeft = timeLimit;
    }
    private void Update()
    {
        /*if(AlarmHasSounded() && alarmSound.isPlaying == false)
        {
            alarmSound.Play();
        }
        if(AlarmHasSounded() == false && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }*/
        if(secondsLeft > 0)
        {
            References.canvas.arenaTimerText.enabled = true;
            secondsLeft -= Time.deltaTime;
            References.canvas.arenaTimerText.text = secondsLeft.ToString("N1");
        }
        else
        {
            References.canvas.arenaTimerText.enabled = false;
        }
    }
}
