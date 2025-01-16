using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class PounceNStretch : MonoBehaviour
{
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;
    public GameObject timerSlider;

    public bool leaping;
    public float leapTime;
    public float leapCurrentTime;

    public float rechargeTime;
    public float currentRechargeTime;

    public float knockBackForce;

    public float knockBackForceUp;
    AnimatedTimerSlider myTimerSlider;
    public GameObject myTimerSliderPrefab;

    public RenderTexture targetTexture;
    public Camera myCam;
    public GameObject myCamGameObject;
    public int myCamTicket;

    void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>(); //����� ��������� �������
        agent = GetComponent<NavMeshAgent>();     //����� ��������� �������
        currentRechargeTime = 0; //����� ����� �������
        leaping = false;

        //�� � �������
        if(myTimerSliderPrefab != null) //����� � ������� �� ���� ������
        {
            GameObject timerSliderObject = Instantiate(myTimerSliderPrefab, References.canvasInWorld.transform);
            myTimerSlider = timerSliderObject.GetComponent<AnimatedTimerSlider>();
            myTimerSlider.ShowTime(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (References.thePlayer)
            {
                //���� ����� ��� �� 10 � ������ � ����� �������
                if (Vector3.Distance(transform.position, References.thePlayer.transform.position) < 10 && currentRechargeTime <= 0)
                {
                    //�������� ������������
                    GetComponent<EnemyBehaviour>().chasePlayerOn = false;

                    agent.enabled = false; //��������� ���������
                    ourRigidBody.isKinematic = false; //����� ����� ���� ������ �� ������
                    ourRigidBody.constraints = RigidbodyConstraints.None;
                    //�������� ��������
                    ourRigidBody.AddForce((transform.forward * knockBackForce) + transform.up * knockBackForceUp);

                    //������� � �������
                    if (myCam != null)
                    {
                        // 1 - ���������� ImgForCamEnemyN + CamBorder
                        References.canvas.GetComponent<EnemyCamSettings>().turnOnTheCamera();
                        // 2 - ������ �������� �� ����� (����� �� ��������� �� ����������)
                        myCamTicket = References.enemyCams.GiveTicket();
                        if (myCamTicket <= 4) //���� ����� ������ �����, �� ����� ��������
                        {
                            targetTexture = References.textureCamBank.targetTextures[myCamTicket];
                            // 3 - ���������� �������� ����� ������
                            myCam.targetTexture = targetTexture;
                            //4 - �������� ���� ������
                            myCamGameObject.SetActive(true);
                        }
                    }
                    leaping = true; //� ������� ��������� ������ �� ���������

                }
            }
        }

        if (leaping)
        {

            leapCurrentTime += Time.deltaTime;

            if (leapCurrentTime >= leapTime)
            {
                GetComponent<EnemyBehaviour>().chasePlayerOn = true;

                agent.enabled = true; //�������� ���������
                ourRigidBody.isKinematic = true; //���������� �������
                leaping = false;
                leapCurrentTime = 0;//���������� �������� � ������������ � �������
                currentRechargeTime = rechargeTime; //������� ����� ����������� ���������� ����������

                if (myCam != null)
                {
                    if (myCamTicket <= 4)
                    {
                        References.canvas.GetComponent<EnemyCamSettings>().turnOffTheCamera(myCamTicket);
                    }
                    myCamGameObject.SetActive(false);
                }

            }
        }
        if (currentRechargeTime > 0)
        {
            currentRechargeTime -= Time.deltaTime;
            if(myTimerSliderPrefab != null) //����� �� ���� ������ � ������� (����� ��������� ������?)
            {
                myTimerSlider.ShowTime(Mathf.Clamp01(currentRechargeTime / rechargeTime));
                myTimerSlider.transform.position = transform.position + Vector3.up * -0.8f;
            }
        }
    }
    private void OnDestroy()
    {
        if (leaping && myCamTicket <= 4)
        {
            if(References.canvas != null)
            {
                References.canvas.GetComponent<EnemyCamSettings>().turnOffTheCamera(myCamTicket);
            }
        }
        if (myTimerSlider != null) //��� ������ ���� �����, ����� �� ���� ������ �� ��������� ������
        {
            Destroy(myTimerSlider.gameObject);
        }
    }
}
