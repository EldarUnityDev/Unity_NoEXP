using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiqueBehaviour : MonoBehaviour
{
    public void BeCollected()
    {
        References.thePlayer.IncreaseScore(1);
        References.alarmManager.RaiseAlertLevel();
        Destroy(gameObject);
    }
}
