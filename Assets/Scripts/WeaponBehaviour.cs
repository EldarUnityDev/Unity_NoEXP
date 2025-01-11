using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour
{
    AudioSource shotAudio;
    public GameObject bullet;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    float secondsSinceLastShot;
    public float timeBeforeReload; //for enemies
    public int damage;

    public float kickAmount;

    public int magSize;
    public int currentMagSize;
    public int magNumber;
    public bool autoMode;

    public GameObject magUIPrefab;
    MagBarBehaviour myMagBar;
    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots; //стрелять можно
        currentMagSize = magSize;
        GameObject magUIObject = Instantiate(magUIPrefab, References.canvas.transform);
        myMagBar = magUIObject.GetComponent<MagBarBehaviour>();
        shotAudio = GetComponent<AudioSource>();

        timeBeforeReload = 3; //for enemies

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.GetComponent<PlayerBehaviour>()) //а то и врагов перезаряжает
            {
                if (Input.GetButtonDown("Reload"))
                {
                    if (currentMagSize <= 0 && magNumber > 0)
                    {
                        ReloadMag();
                        ReduceMagNumber();
                    }
                }
            }
        }
        
        if (secondsSinceLastShot < secondsBetweenShots)
        {
            secondsSinceLastShot += Time.deltaTime;
        }

        myMagBar.ShowMagFraction(currentMagSize / magSize);
    }

    public void BePickedUpByPlayer()
    {
        //add to player's internal list
        transform.rotation = References.thePlayer.transform.rotation;
        transform.position = References.thePlayer.hands.transform.position;
        //Parent it to us - attach it so it moves with us
        transform.SetParent(References.thePlayer.transform);
        //чтобы старое и новое оружие одновременно не оказывалось в руках
        References.thePlayer.PickUpWeapon(this);
    }

    public void ReloadMag()
    {
        currentMagSize = magSize;
        secondsSinceLastShot = secondsBetweenShots;
    }
    public void ReduceMagNumber()
    {
        magNumber--;
    }
    public void Fire(Vector3 targetPosition)
    {
        if (secondsSinceLastShot >= secondsBetweenShots && currentMagSize > 0)
        {
            //ready to fire
            for(int i = 0; i < numberOfProjectiles; i++)
            {
                //Create a copy (what, where, where to look)
                References.alarmManager.SoundTheAlarm();
                if(shotAudio != null)
                {
                    shotAudio.Play();

                }
                References.cameraTools.joltVector = transform.forward * kickAmount;
                GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
                newBullet.GetComponent<BulletBehaviour>().bulletDamage = damage;
                if (gameObject.transform.parent.GetComponent<SniperBehaviour>())
                {
                    newBullet.transform.parent = gameObject.transform;
                }
                //offset that target amount by a random amount, according to our inaccuracy
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
                Vector3 inaccuratePosition = targetPosition;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(inaccuratePosition);
                secondsSinceLastShot = 0;
                newBullet.name = i.ToString();
            }
            currentMagSize--;
        }
    }
    public void Drop()
    {
        transform.parent = null;
        //перемещаем в основную сцену, чтобы не сохранялось при переходе на след уровень
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        GetComponent<Useable>().enabled = true;
    }
}
