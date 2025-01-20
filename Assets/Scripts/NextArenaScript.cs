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
    public bool comeBack;
    public bool stillThere;

    public float multiplier;
    public float desiredUp;
    public float desiredZ;
    public float oriZ;
    Vector3 desiredArenaPlace;

    void Start()
    {
        stillThere = true;
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        desiredUp = transform.position.y + 2;
        desiredArenaPlace = new Vector3(75, 0, -15);
        desiredZ = -15;
        oriZ = transform.position.z;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerBehaviour>() != null && stillThere)
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
            if(transform.position.y < desiredUp)
            {
                transform.position += new Vector3(0, 4, 0) * Time.deltaTime;
                References.thePlayer.transform.position += new Vector3(0, 4, 0) * Time.deltaTime;
            }
            
            if(nextArena.transform.position.z > desiredZ)
            {
                nextArena.transform.position -= new Vector3(0, 0, 15) * Time.deltaTime;
                currentArena.transform.position -= new Vector3(0, 0, 15) * Time.deltaTime;

            }
            if(nextArena.transform.position.z < desiredZ)
            {
                //nextArena.transform.position = desiredArenaPlace;
                nextArenaSwitch = false;
                stillThere = false;
                transform.position -= new Vector3(0, 2, 0);
                References.thePlayer.transform.position -= new Vector3(0, 2, 0);
            }
        }
    }
}


