using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void ReplenishHealth()
    {
        if(References.thePlayer != null)
        {
            References.thePlayer.GetComponent<HealthSystem>().ReplenishHealth();
            Destroy(gameObject);
        }
    }
}
