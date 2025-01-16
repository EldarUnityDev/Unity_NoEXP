using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAreaScript : MonoBehaviour
{
    public float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;
    public GameObject currentArena;
    public GameObject nextArena;
    public bool nextArenaSwitch;

    void Start()
    {
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerBehaviour>() != null)
        {
            secondsBeforeNextLevel -= Time.deltaTime;
            if (secondsBeforeNextLevel <= 0)
            {
                //move the level
                nextArenaSwitch = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }
    }
    private void Update()
    {
        if (nextArenaSwitch)
        {
            //z = -25
            
            nextArena.transform.position -= new Vector3(0,0,9) * Time.deltaTime;
        }
    }
}


