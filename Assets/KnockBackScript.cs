using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockBackScript : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;
    public bool knockBackOn; // ����� �������
    public float knockbackTime; // ����� �� ���������� 
    public float knockbackCurrentTime; //������� �����
    public bool beingKnockedBack; //��� ������� ������� �� ����������
    // Start is called before the first frame update
    void Start()
    {
        knockbackCurrentTime = 0;
        beingKnockedBack = false;
        ourRigidBody = GetComponent<Rigidbody>(); //����� ��������� �������
        agent = GetComponent<NavMeshAgent>();     //����� ��������� �������
    }

    // Update is called once per frame
    void Update()
    {

 

        /*if (overchargedStep >= overchargeLimit) //������� ��������� �� ��� ������������
        {

            GetComponent<HealthSystem>().KillMe();
        }
        */
        if (beingKnockedBack == true) //������ �� ������
        {
            knockbackCurrentTime += Time.deltaTime;
            if (knockbackCurrentTime >= knockbackTime)
            {
                StopKnockback();
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
        ourRigidBody.isKinematic = true; //???????????????????????????????????????????
        knockbackCurrentTime = 0; //�������� ������

        beingKnockedBack = false;
    }
}
