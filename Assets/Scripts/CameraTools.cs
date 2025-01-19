using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    Vector3 normalPosition;
    Vector3 desiredPosition;
    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;
    public Vector3 cameraOffset;
    private void Awake()
    {
        References.cameraTools = this;
    }
    private void Start()
    {
        normalPosition = transform.position;
    }

    void Update()
    {


        Vector3 shakeVector = new Vector3(GetRnadmoShakeAmount(), GetRnadmoShakeAmount(), GetRnadmoShakeAmount());
        desiredPosition = normalPosition + joltVector + shakeVector;

        //set our position to Jolted position
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, maxMoveSpeed*Time.deltaTime);
        


        //joltvector decreases
        joltVector *= joltDecayFactor;
        shakeAmount *= shakeDecayFactor;
    }

    float GetRnadmoShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
