using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    private void Awake()
    {
        References.startingPosition = this;
    }
    void Start()
    {
        References.thePlayer.transform.position = transform.position;
        Destroy(gameObject);
    }

}
