using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockBackScript : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;
    public bool knockBackOn; // можно толкать
    public float knockbackTime; // время до отключения 
    public float knockbackCurrentTime; //текущее время
    public bool beingKnockedBack; //для запуска таймера на выключение
    // Start is called before the first frame update
    void Start()
    {
        knockbackCurrentTime = 0;
        beingKnockedBack = false;
        ourRigidBody = GetComponent<Rigidbody>(); //чтобы управлять физикой
        agent = GetComponent<NavMeshAgent>();     //чтобы управлять физикой
    }

    // Update is called once per frame
    void Update()
    {

 

        /*if (overchargedStep >= overchargeLimit) //считаем попадания по нам электрошоком
        {

            GetComponent<HealthSystem>().KillMe();
        }
        */
        if (beingKnockedBack == true) //таймер на отскок
        {
            knockbackCurrentTime += Time.deltaTime;
            if (knockbackCurrentTime >= knockbackTime)
            {
                StopKnockback();
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
        ourRigidBody.isKinematic = true; //???????????????????????????????????????????
        knockbackCurrentTime = 0; //обнуляем таймер

        beingKnockedBack = false;
    }
}
