using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public string firstLevelName;
    public float secondsBeforeNextLevel;
    public float graceTimeBeforeNextLevel;

    public float secondsBeforeShowingDeathMenu;
    bool shownDeathMenu;

    private void Awake()
    {
        References.myLevelManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       // SceneManager.LoadScene(firstLevelName);
        secondsBeforeNextLevel = 3;
        graceTimeBeforeNextLevel = 3;
        shownDeathMenu = false;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Start Game");
        Time.timeScale = 1;
    }
    public void StartTurorial()
    {
        SceneManager.LoadScene("Start Tutorial");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies are dead go to next level
        if (References.allEnemies.Count == 0 && References.alarmManager.enemiesHaveSpawned)
        {
            if (secondsBeforeNextLevel > 0)
            {
                secondsBeforeNextLevel -= Time.deltaTime;

                //stop the alarm
                References.alarmManager.StopTheAlarm();

                if (secondsBeforeNextLevel <= 0)
                {
                    if (References.levelGenerator.showMenuWhenDone)
                    {
                        References.canvas.ShowMainMenu();
                    }
                    else
                    {
                        SceneManager.LoadScene(References.levelGenerator.nextLevelName);

                    }

                }
            }
        }
        else
        {
            secondsBeforeNextLevel = graceTimeBeforeNextLevel;
        }


        //When we are dead
        if(References.thePlayer == null && shownDeathMenu == false)
        {
            secondsBeforeShowingDeathMenu -= Time.deltaTime;
            if(secondsBeforeShowingDeathMenu <= 0)
            {
                References.canvas.ShowScoreMenu();
                shownDeathMenu = true;
            }
        }
    }
}
