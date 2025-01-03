using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;

    public int overchargedStep; //FOR TASER
    public int overchargeLimit;

    public bool knockBackOn; // ����� �������
    public float knockbackTime; // ����� �� ���������� 
    public float knockbackCurrentTime; //������� �����

    public bool beingKnockedBack; //��� ������� ������� �� ����������

    public bool chasePlayerOn;


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
        overchargedStep = 0;
        knockbackCurrentTime = 0;
        beingKnockedBack = false;
        chasePlayerOn = true;
    }

    protected virtual void Update()
    {
        if (chasePlayerOn && !beingKnockedBack) //��������� ������� � ���� � ������
        {
            ChasePlayer();
        }
        
        if (overchargedStep >= overchargeLimit) //������� ��������� �� ��� ������������
        {
            GetComponent<HealthSystem>().TakeDamage(GetComponent<HealthSystem>().maxHealth);
        }

        if (beingKnockedBack == true) //������ �� ������
        {
            knockbackCurrentTime += Time.deltaTime;
            if (knockbackCurrentTime >= knockbackTime)
            {
                StopKnockback();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            Debug.Log("TIME TO DIE");
            GetComponent<HealthSystem>().KillMe();
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



    //������� ���� : BulletBehaviour
    public void GetKnockedBack(Vector3 knockbackVector, float knockbackPower)
    {
        knockBackOn = false; //���� �������, ����� ������� ������

        agent.enabled = false; //��������� ���������

        ourRigidBody.isKinematic = false; //����� ����� ���� ������ �� ������
        ourRigidBody.constraints = RigidbodyConstraints.None;

        ourRigidBody.velocity = knockbackVector * knockbackPower;

        beingKnockedBack = true; //� ������� ��������� ������ �� ���������
    }
    public void StopKnockback()
    {
        knockBackOn = true; //������� �� ������
        agent.enabled = true;
        ourRigidBody.isKinematic = true;
        knockbackCurrentTime = 0; //�������� ������

        beingKnockedBack = false;
    }
}
