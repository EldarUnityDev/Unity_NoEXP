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
        ourRigidBody = GetComponent<Rigidbody>(); //����� ��������� �������
        agent = GetComponent<NavMeshAgent>();     //����� ��������� �������
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerBehaviour>())
        {
            agent.enabled = false; //��������� ���������, �� ���� ���������� ������ ���������� ������ � ������� ������
            transform.parent = References.thePlayer.transform; //������������ � ������
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
