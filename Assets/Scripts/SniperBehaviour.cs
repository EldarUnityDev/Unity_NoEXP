using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class SniperBehaviour : EnemyBehaviour
{

    public float visionRange;
    public WeaponBehaviour myWeapon;

    public float reactionTime;
    float secondsSeeingPlayer;
    public float camOffDelay;
    public bool camFading;
    public bool loaded;

    public RenderTexture targetTexture;
    public Camera myCam;
    public GameObject myCamGameObject;

    public RenderTexture targetTriggerTexture;
    public Camera myTriggerCam;
    public GameObject myTriggerCamGameObject;

    public int myCamTicket;
    public bool shotsFired;

    //protected - this function has to be the same scope as parent "not private", "not public"
    //override - we know our parent has this version, but we're changing it
    //base.Start() - run our parent's version of this

    protected override void Start()
    {
        secondsSeeingPlayer = 0;
        base.Start();
        shotsFired = false;
    }

    protected override void Update()
    {
        //без этого условия при смерти игрока у врагов будет ошибка
        if (References.thePlayer != null)
        {
            //where to go = destination - the origin
            Vector3 vectorToPlayer = PlayerPosition() - transform.position;
            //follow the player
            ChasePlayer(); // destination + lookAt if CAN see

            if (loaded) //чтобы отключать в инспекторе для тестов
            {
                // if can see player, shoot
                if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
                {
                    if (Vector3.Distance(transform.position, PlayerPosition()) <= visionRange)
                    {
                        TurnMyCamOn();
                        secondsSeeingPlayer += Time.deltaTime;

                        TurnTriggerCamOn();
                        References.triggerAnimation.timeToShoot = true;
                        References.fingerAnimation.timeToShoot = true;
                        Debug.Log(secondsSeeingPlayer / reactionTime);
                        // References.triggerAnimation.PullTheTriggerFraction(secondsSeeingPlayer / reactionTime);
                        //References.fingerAnimation.PullTheTriggerFraction(secondsSeeingPlayer / reactionTime);


                        if (secondsSeeingPlayer >= reactionTime) //замедляем реакцию, хотя смотрит он на тебя сразу из-за ChasePlayer()
                        {
                            //References.sniperCamTexture = myCam.targetTexture;

                            myWeapon.Fire(PlayerPosition());
                            References.triggerAnimation.ResetShootingPosition();
                            References.fingerAnimation.ResetShootingPosition();
                        }
                    }
                }
                else
                {
                    if (secondsSeeingPlayer > 0)
                    {

                        camFading = true; //не выключаем камеру, но запускаем задержку
                    }
                    secondsSeeingPlayer = 0;
                }
                if (camFading)
                {
                    camOffDelay -= Time.deltaTime; //пошёл таймер на выключение камеры
                }
                if (camOffDelay <= 0.1f) //выключаем таймер и камеру
                {
                    camFading = false;
                    TurnMyCamOff();
                    camOffDelay = 1;
                }
            }
        }


    }
    public void TurnMyCamOn()
    {
        if (myCam != null && !myCamGameObject.activeInHierarchy)
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
    }
    public void TurnTriggerCamOn()
    {
        if (myTriggerCam != null && !myTriggerCamGameObject.activeInHierarchy)
        {
            // 1 - активируем ImgForCamEnemyN + CamBorder
            References.canvas.GetComponent<EnemyCamSettings>().turnOnTheTriggerCamera();
            // 2 - достаём текстуру из банка (лучше бы загружали из библиотеки)
                targetTexture = References.textureCamBank.targetTextures[4];
                // 3 - присвоение текстуры нашей камере
                myTriggerCam.targetTexture = targetTexture;
                //4 - включаем саму камеру
                myTriggerCamGameObject.SetActive(true);
            
        }
    }
    public void TurnMyCamOff()
    {
        if (myCam != null)
        {
            References.canvas.GetComponent<EnemyCamSettings>().turnOffTheCamera(myCamTicket);
            myCamGameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (References.canvas != null)
        {
            References.canvas.GetComponent<EnemyCamSettings>().turnOffTheCamera(myCamTicket);
        }


    }
}
