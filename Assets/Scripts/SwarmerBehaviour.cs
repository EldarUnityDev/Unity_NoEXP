using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehaviour : MonoBehaviour
{
    public GameObject exploisionPrefab;
    protected void OnCollisionEnter(Collision collision)
    {
        //даём переменную тому, с кем столкнулись
        GameObject theirGameObject = collision.gameObject;
        PlayerBehaviour possiblePlayer = theirGameObject.GetComponent<PlayerBehaviour>();
        //если это игрок 
        if (possiblePlayer == References.thePlayer)
        {
            //this hurts the player
            Instantiate(exploisionPrefab, transform.position, transform.rotation);
            //this destroys the enemy
            Destroy(gameObject); //- уничтожает врага (в прошлом пулю) на касание
        }
    }
}
