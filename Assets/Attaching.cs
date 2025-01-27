using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attaching : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;
    public bool timeToDetach;
    public bool attached;

    // Start is called before the first frame update
    void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>(); //чтобы управлять физикой
        agent = GetComponent<NavMeshAgent>();     //чтобы управлять физикой
    }
    private void OnCollisionEnter(Collision collision) //прицепляемся на касание
    {
        if (collision.gameObject.GetComponentInParent<PlayerBehaviour>() && !attached && GetComponent<HealthSystem>().currentHealth>0) //if collision == player
        {
            agent.enabled = false; //выключаем навигацию
            GetComponent<HealthSystem>().readyToDie = false;
            GetComponent<EnemyBehaviour>().enabled = false; //отключаем преследование
            transform.parent = References.thePlayer.transform; //прицепляемся к игроку
            ourRigidBody.isKinematic = true;
            attached = true;
            References.thePlayer.GetComponent<PlayerBehaviour>().slowedDown = true;
        }
    }
    private void Update()
    {
        if (attached)
        {
            //transform.position = References.thePlayer.transform.position + References.thePlayer.transform.forward * 1.4f;
            if(GetComponentInParent<PlayerBehaviour>() == null) {
                attached = false;
                References.thePlayer.GetComponent<PlayerBehaviour>().slowedDown = false;
            }
        }
    }
}
