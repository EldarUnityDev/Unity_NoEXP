using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GuardBehaviour : EnemyBehaviour
{

    public bool onPatrol;
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public Light myLighty;
    public float turnSpeed;
    public WeaponBehaviour myWeapon;
    public GameObject myLight;

    public float reactionTime;
    float secondsSeeingPlayer;

    public bool loaded;



    //protected - this function has to be the same scope as parent "not private", "not public"
    //override - we know our parent has this version, but we're changing it
    //base.Start() - run our parent's version of this

    protected override void Start()
    {
        secondsSeeingPlayer = 0;
        base.Start();
    }

    void GoToRandomNavPoint()
    {
        //��������� ����� ������ �� ����������
        int randomNavPointIndex = Random.Range(0, References.navPoints.Count);
        //�������, ���� ����
        agent.destination = References.navPoints[randomNavPointIndex].transform.position;
    }

    public void KnockoutAttempt()
    {
        if(References.alarmManager.AlarmHasSounded() == false)
        {
            GetComponent<HealthSystem>().KillMe();
            References.alarmManager.RaiseAlertLevel();
        }
    }
    public void StealMyLight()
    {
        if (myLight != null)
        {
            //Destroy(myLight);
            myLight.transform.parent = References.thePlayer.transform;
            myLight.transform.position = References.thePlayer.transform.position + new Vector3 (0.75f,0,0);
            myLight.transform.rotation = Quaternion.Euler(GetRandomRotation(), GetRandomRotation(), GetRandomRotation());
            //set the record to player's loot
        }
    }

    float GetRandomRotation()
    {
        return Random.Range(-180, +180);
    }

    protected override void Update()
    {

                //��� ����� ������� ��� ������ ������ � ������ ����� ������
        if (References.thePlayer != null)
        {
            //where to go = destination - the origin
            Vector3 vectorToPlayer = PlayerPosition() - transform.position;
            myLighty.color = Color.white;

            if (alerted)
            {
                //follow the player
                myLighty.color = Color.red;
                ChasePlayer(); // destination + lookAt if CAN see

                if (loaded) //����� ��������� � ���������� ��� ������
                {
                    // if can see player, shoot
                    if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
                    {
                        secondsSeeingPlayer += Time.deltaTime;
                        if(secondsSeeingPlayer >= reactionTime) //��������� �������, ���� ������� �� �� ���� ����� ��-�� ChasePlayer()
                        {
                            myWeapon.Fire(PlayerPosition());
                        }
                    }
                    else
                    {
                        secondsSeeingPlayer = 0;
                    }
                }

                //provides breaks before mags
                if (myWeapon.currentMagSize == 0)
                {
                    myWeapon.timeBeforeReload -= Time.deltaTime;
                }
                if (myWeapon.timeBeforeReload <= 0)
                {
                    myWeapon.ReloadMag();
                    myWeapon.timeBeforeReload = 3;
                }
            }
/*
            //���� ��� ������
            if (beingKnockedBack == true)
            {//�������, ����� ���������� �����
                knockbackCurrentTime += Time.deltaTime;
                if (knockbackCurrentTime >= knockbackTime)
                {
                    StopKnockback();
                }
            }*/
            //kostil
            if (agent.enabled)
            {
                if (onPatrol == true && agent.remainingDistance < 3)
                {
                    GoToRandomNavPoint();
                }
                if (!onPatrol) //���� �� �����������, �� ��������
                {
                    //Rotate
                    Vector3 lateralOffset = transform.right * Time.deltaTime;
                    transform.LookAt(transform.position + transform.forward + lateralOffset * turnSpeed);
                }
                
                //checking if we can see the player
                if (Vector3.Distance(transform.position, PlayerPosition()) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        //Raycast(starting point, direction, distance to check, what we want to hit)
                        //
                        if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer) == false)
                        {
                            myLighty.color = Color.red;
                            alerted = true;
                            //References.alarmManager.SoundTheAlarm();
                        }
                    }
                }
            }
        }
    }
}
