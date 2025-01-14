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

    public bool knockBackOn; // можно толкать
    public float knockbackTime; // время до отключения 
    public float knockbackCurrentTime; //текущее время
    public bool beingKnockedBack; //для запуска таймера на выключение

    public bool chasePlayerOn;
    public bool explodeOnTouch;

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
        knockbackCurrentTime = 0;
        beingKnockedBack = false;
        //chasePlayerOn = true;

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
            if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
            {
                chasePlayerOn = true;
            }
        }

        if (chasePlayerOn && !beingKnockedBack) //постоянно смотрим и идем к игроку
        {
            ChasePlayer();
        }
        
        if (overchargedStep >= overchargeLimit) //считаем попадания по нам электрошоком
        {
            GetComponent<HealthSystem>().TakeDamage(GetComponent<HealthSystem>().maxHealth);
        }

        if (beingKnockedBack == true) //таймер на отскок
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
        if (explodeOnTouch)
        {
            if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                GetComponent<HealthSystem>().KillMe();
            }
        }
    }

    protected void ChasePlayer()
    {
        //без этого условия при смерти игрока у врагов будет ошибка
        if (References.thePlayer != null)
        {
            //where to go = destination - the origin
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;

            // ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed;
            if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
            {
                Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                transform.LookAt(playerPositionAtOurHeight); //сюда смотри

            }

            //follow the player
            if (agent.enabled)
            {
                agent.destination = playerPosition;          //сюда иди
            }
        }
    }



    //толкает пуля : BulletBehaviour
    public void GetKnockedBack(Vector3 knockbackVector, float knockbackPower)
    {
        knockBackOn = false; //пока толкают, снова толкать нельзя

        agent.enabled = false; //выключаем навигацию

        ourRigidBody.isKinematic = false; //чтобы можно было влиять на физику
        ourRigidBody.constraints = RigidbodyConstraints.None;

        ourRigidBody.velocity = knockbackVector * knockbackPower;

        beingKnockedBack = true; //в апдейте запускает таймер на остановку
    }
    public void StopKnockback()
    {
        knockBackOn = true; //обратно на рельсы
        agent.enabled = true;
        ourRigidBody.isKinematic = true;
        knockbackCurrentTime = 0; //обнуляем таймер

        beingKnockedBack = false;
    }
}
