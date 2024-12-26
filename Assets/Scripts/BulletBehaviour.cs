using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public bool ableToRicochet;
    public float bulletSpeed;
    public float secondsUntilDestroyed;
    public float bulletDamage;
    public Vector3 knockbackDirection;
    public float knockbackPower; //20
    void Start()
    {
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = transform.forward * bulletSpeed;
        knockbackDirection = transform.forward;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        secondsUntilDestroyed -= Time.deltaTime;
        if (secondsUntilDestroyed < 1)
        {
            transform.localScale *= secondsUntilDestroyed;

        }
        if (secondsUntilDestroyed<=0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();

        //Получение урона
        GameObject theirGameObject = collision.gameObject;
        HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(bulletDamage);
        }
        if (theirGameObject.GetComponent<EnemyBehaviour>() != null)
        {
            //Knockback - Если можно толкнуть
            if (theirGameObject.GetComponent<EnemyBehaviour>().knockBackOn)
            {
                theirGameObject.GetComponent<EnemyBehaviour>().GetKnockedBack(knockbackDirection, knockbackPower);
            }

            Destroy(gameObject);
        }
        if (ableToRicochet)
        {
            if(theirGameObject.GetComponent<BulletBehaviour>() != null)
            {
                //offset that target amount by a random amount, according to our inaccuracy
                Vector3 inaccuratePosition = transform.forward;
                inaccuratePosition.x += Random.Range(-90, 90);
                inaccuratePosition.z += Random.Range(-90, 90);
                transform.LookAt(inaccuratePosition);
                ourRigidBody.velocity = transform.forward* bulletSpeed;

            }
        }
       /* if (theirGameObject.GetComponent<TargetDummyBehaviour>() != null)
        {
            //theirGameObject.transform.rotation += 90 degrees
            theirGameObject.transform.localScale *= 0.5f;
        }*/
    }
    public Vector3 RandomizeRicochetDirection()
    {
        //random X Z
        //        ourRigidBody.velocity = knockbackVector * knockbackPower;

        //modifyee = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
        Vector3 pipa = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));


        return pipa;

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject theirGameObject = other.gameObject;
        TargetDummyBehaviour curTDB = theirGameObject.GetComponent<TargetDummyBehaviour>();
        if (theirGameObject.GetComponent<TargetDummyBehaviour>() != null && curTDB.isActive == true)
        {
            curTDB.isActive = false;
            //theirGameObject.transform.rotation += 90 degrees
            theirGameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
