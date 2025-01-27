using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;

    public int overchargedStep; //FOR TASER
    public int overchargeLimit;


    public bool chasePlayerOn;
    public bool explodeOnTouch;

    public bool initialContactDone;

    public bool attachable;

    protected void OnEnable()
    {
        References.allEnemies.Add(this);
  
    }
    protected void OnDisable()
    {
        References.allEnemies.Remove(this);
    }
    //protected - can be used by parent and child (not anyone else)
    //we MUST put PROTECTED before everything the children might use
    //virtual - can be overriden by our children - if they don't override it, they just use this version
    protected virtual void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        overchargedStep = 0;


    }
    protected Vector3 PlayerPosition()
    {
        return References.thePlayer.transform.position;
    }
    protected virtual void Update()
    {
        if(References.thePlayer != null)
        {
            Vector3 vectorToPlayer = PlayerPosition() - transform.position;

            // if can see player, chase
            if (!initialContactDone)   //���� ��� �� ��������
            {
                if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
                {
                    chasePlayerOn = true;
                    initialContactDone = true;
                }
            }
        }
        if (chasePlayerOn) //��������� ������� � ���� � ������
        {
            ChasePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explodeOnTouch)
        {
            if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                GetComponent<HealthSystem>().KillMe();
            }
        }
        if (attachable)
        {
            if (!explodeOnTouch && collision.gameObject.GetComponent<PlayerBehaviour>() != null && !GetComponent<Attaching>().attached)
            {
                collision.gameObject.GetComponent<HealthSystem>().TakeDamage(1);
            }
        }


    }

    protected void ChasePlayer()
    {
        //��� ����� ������� ��� ������ ������ � ������ ����� ������
        if (References.thePlayer != null)
        {
            //where to go = destination - the origin
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;

            // ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed;
            if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
            {
                Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                transform.LookAt(playerPositionAtOurHeight); //���� ������
            }

            //follow the player
            if (agent.enabled)
            {
                agent.destination = playerPosition;          //���� ���
            }
        }
    }
}
