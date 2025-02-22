using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextArenaScript : MonoBehaviour
{
    //public SceneAsset nextLevel;
    
    public float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;
    // Start is called before the first frame update
    void Start()
    {
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            Debug.Log("+inside me");
            secondsBeforeNextLevel -= Time.deltaTime;

            if (secondsBeforeNextLevel <= 0)
            {
                // go to the next level
              //  SceneManager.LoadScene(nextLevel.name);

            }
        }
        else
        {
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
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
       // Debug.Log(secondsBeforeNextLevel);
    }
}
    
