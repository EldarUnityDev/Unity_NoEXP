using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float secondsToExist;
    float secondsWeveBeenAlive;
    public float damage;
    Vector3 maxScale;
    public GameObject soundObject;
    void Start()
    {
        maxScale = transform.localScale;
        transform.localScale = Vector3.zero;
        secondsWeveBeenAlive = 0;
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        secondsWeveBeenAlive += Time.deltaTime;
        
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, secondsWeveBeenAlive/secondsToExist);

        if (secondsWeveBeenAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
        
         /*     TOM'S SOLUTION
        secondsWeveBeenAlive += Time.fixedDeltaTime;
        Debug.Log(secondsWeveBeenAlive);
        if(secondsWeveBeenAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
        */
    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject theirGameObject = collision.gameObject;
        if (theirGameObject.GetComponent<HealthSystem>() != null)
        {
            HealthSystem theirHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(damage);
            }
        }
    }
}
