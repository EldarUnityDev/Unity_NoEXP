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
        ourRigidBody = GetComponent<Rigidbody>(); //чтобы управлять физикой
        agent = GetComponent<NavMeshAgent>();     //чтобы управлять физикой
        currentRechargeTime = 0; //можно сразу прыгать
        leaping = false;

        //всё о таймере
        if(myTimerSliderPrefab != null) //чтобы у простых не было ошибки
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
                //если ближе чем на 10 к игроку и можно прыгать
                if (Vector3.Distance(transform.position, References.thePlayer.transform.position) < 10 && currentRechargeTime <= 0)
                {
                    //перестаём преследовать
                    GetComponent<EnemyBehaviour>().chasePlayerOn = false;

                    agent.enabled = false; //выключаем навигацию
                    ourRigidBody.isKinematic = false; //чтобы можно было влиять на физику
                    ourRigidBody.constraints = RigidbodyConstraints.None;
                    //единожды толкнуть
                    ourRigidBody.AddForce((transform.forward * knockBackForce) + transform.up * knockBackForceUp);

                    //ебанина с камерой
                    if (myCam != null)
                    {
                        // 1 - активируем ImgForCamEnemyN + CamBorder
                        References.canvas.GetComponent<EnemyCamSettings>().turnOnTheCamera();
                        // 2 - достаём текстуру из банка (лучше бы загружали из библиотеки)
                        myCamTicket = References.enemyCams.GiveTicket();
                        if (myCamTicket <= 4) //если билет больше камер, не нужна текстура
                        {
                            targetTexture = References.textureCamBank.targetTextures[myCamTicket];
                            // 3 - присвоение текстуры нашей камере
                            myCam.targetTexture = targetTexture;
                            //4 - включаем саму камеру
                            myCamGameObject.SetActive(true);
                        }
                    }
                    leaping = true; //в апдейте запускает таймер на остановку

                }
            }
        }

        if (leaping)
        {

            leapCurrentTime += Time.deltaTime;

            if (leapCurrentTime >= leapTime)
            {
                GetComponent<EnemyBehaviour>().chasePlayerOn = true;

                agent.enabled = true; //включаем навигацию
                ourRigidBody.isKinematic = true; //возвращаем обратно
                leaping = false;
                leapCurrentTime = 0;//изменяется таймером и сравнивается с лимитом
                currentRechargeTime = rechargeTime; //сколько нужно увеличивать предыдущую переменную

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
            if(myTimerSliderPrefab != null) //чтобы не было ошибки у простых (нужен отдельный скрипт?)
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
        if (myTimerSlider != null) //это только ради юнити, чтобы не было ошибки по окончанию сессии
        {
            Destroy(myTimerSlider.gameObject);
        }
    }
}
