using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlinthBehaviour : MonoBehaviour
{

    Useable myUseable;
    public TextMeshProUGUI myLabel;
    public Transform spotForItem;
    public GameObject cage;

    public float secondsToLock;

    private void OnEnable()
    {
        References.plinths.Add(this);
    }
    private void OnDisable()
    {
        References.plinths.Remove(this);
    }

    public void AssignItem (GameObject item)
    {
        myUseable = item.GetComponent<Useable>();
        myLabel.text = myUseable.displayName;
        myUseable.transform.position = spotForItem.transform.position;
        myUseable.transform.rotation = spotForItem.transform.rotation;
    }

    private void Update()
    {
        if(secondsToLock > 0 && References.alarmManager.AlarmHasSounded())
        {
            secondsToLock -= Time.deltaTime;
            if(secondsToLock <= 0)
            {
                cage.SetActive(true);
                myLabel.text = "ALARM";
                //если экспонат существует и игрок его ещё не взял
                if (myUseable != null && myUseable.enabled)
                {
                    Destroy(myUseable.gameObject);
                }
            }
        }
    }

}
