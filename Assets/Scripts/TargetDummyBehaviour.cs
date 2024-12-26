using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetDummyBehaviour : MonoBehaviour
{
    public float dummySpeed;
    public Boolean isActive;
    public Rigidbody ourRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        ourRigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (References.thePlayer != null && isActive == true)
        {
            //where to go = destination - the origin
            Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position;
            ourRigidBody.velocity = vectorToPlayer.normalized * dummySpeed;
        }
        if (isActive == false)
        {
            ourRigidBody.velocity = Vector3.zero;

        }
    }
}
