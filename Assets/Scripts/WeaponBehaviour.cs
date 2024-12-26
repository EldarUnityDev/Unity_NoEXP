using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    AudioSource shotAudio;
    public GameObject bullet;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    float secondsSinceLastShot;
    public float timeBeforeReload; //for enemies

    public float kickAmount;

    public int magSize;
    public int currentMagSize;

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
                if (Input.GetButtonDown("Fire1"))
                {
                    if (currentMagSize <= 0)
                    {
                        ReloadMag();
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
        References.alarmManager.RaiseAlertLevel();
        //add to player's internal list
        References.thePlayer.weapons.Add(this);
        transform.position = References.thePlayer.transform.position;
        transform.rotation = References.thePlayer.transform.rotation;
        //Parent it to us - attach it so it moves with us
        transform.SetParent(References.thePlayer.transform);
        //чтобы старое и новое оружие одновременно не оказывалось в руках
        References.thePlayer.SelectLatestWeapon();
        References.canvas.mainWeaponPanel.AssignWeapon(this);
    }

    public void ReloadMag()
    {
        currentMagSize = magSize;
        secondsSinceLastShot = secondsBetweenShots;
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
                References.screenShake.joltVector = transform.forward * kickAmount;
                GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
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
}
