using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAreaScript : MonoBehaviour
{
    //public SceneAsset nextLevel;
    public float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;
    // Start is called before the first frame update
    void Start()
    {
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;

    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject theirGameObject = collision.gameObject;
        if (theirGameObject.GetComponent<PlayerBehaviour>() != null)
        {
            secondsBeforeNextLevel -= Time.deltaTime;

            if (secondsBeforeNextLevel <= 0)
            {
                // go to the next level
              //  SceneManager.LoadScene(nextLevel.name);
            }
        }
        else
        {
            //Not all enemies are dead

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
    
