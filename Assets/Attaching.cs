using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attaching : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>(); //чтобы управлять физикой
        agent = GetComponent<NavMeshAgent>();     //чтобы управлять физикой
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerBehaviour>())
        {
            agent.enabled = false; //выключаем навигацию, но если выстрелить сейчас запустится нокбэк и включит агента
            transform.parent = References.thePlayer.transform; //прицепляемся к игроку
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
