using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehaviour : MonoBehaviour
{
    public GameObject exploisionPrefab;
    protected void OnCollisionEnter(Collision collision)
    {
        //��� ���������� ����, � ��� �����������
        GameObject theirGameObject = collision.gameObject;
        PlayerBehaviour possiblePlayer = theirGameObject.GetComponent<PlayerBehaviour>();
        //���� ��� ����� 
        if (possiblePlayer == References.thePlayer)
        {
            //this hurts the player
            Instantiate(exploisionPrefab, transform.position, transform.rotation);
            //this destroys the enemy
            Destroy(gameObject); //- ���������� ����� (� ������� ����) �� �������
        }
    }
}
