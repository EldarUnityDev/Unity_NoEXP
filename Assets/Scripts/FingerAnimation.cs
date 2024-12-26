using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerAnimation : MonoBehaviour
{
    public float zRotation;
    public bool timeToShoot;
    // Start is called before the first frame update
    void Start()
    {
        References.fingerAnimation = this;

        zRotation = 5;
        //timeToShoot = false;
        transform.localRotation = Quaternion.Euler(-90, -88.4f, zRotation);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeToShoot)
        {
            PullTheTrigger();
        }
    }
    public void PullTheTrigger()
    {
        transform.localRotation = Quaternion.Euler(-90, -88.4f, zRotation);
        if (zRotation < 53)
        {
            zRotation += 0.5f;
        }
        else
        {
            zRotation = 5;
        }
    }
    public void PullTheTriggerFraction(float fraction)
    {
        transform.localRotation = Quaternion.Euler(-90, -88.4f, 5 + 48*fraction);
 
    }
    public void ResetShootingPosition()
    {
        zRotation = 5;
        timeToShoot = false;
    }
}