using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    public float zRotation;
    public bool timeToShoot;
    // Start is called before the first frame update
    void Start()
    {
        References.triggerAnimation = this;
        zRotation = 178;
        //timeToShoot = false;
        transform.localRotation = Quaternion.Euler(0, 0, zRotation);

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
        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
        if (zRotation > 130)
        {
            zRotation -= 0.5f;
        }
        else
        {
            zRotation = 178;
        }
    }
    public void PullTheTriggerFraction(float fraction)
    {
        transform.localRotation = Quaternion.Euler(-90, -88.4f, 178 - 48 * fraction);
          }
    public void ResetShootingPosition()
    {
        zRotation = 178;
        timeToShoot = false;
    }
}
