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
        ourRigidBody = GetComponent<Rigidbody>(); //����� ��������� �������
        agent = GetComponent<NavMeshAgent>();     //����� ��������� �������
    }
    private void OnCollisionEnter(Collision collision) //������������ �� �������
    {
        if (collision.gameObject.GetComponentInParent<PlayerBehaviour>() && !attached && GetComponent<HealthSystem>().currentHealth>0) //if collision == player
        {
            agent.enabled = false; //��������� ���������
            GetComponent<HealthSystem>().readyToDie = false;
            GetComponent<EnemyBehaviour>().enabled = false; //��������� �������������
            transform.parent = References.thePlayer.transform; //������������ � ������
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
