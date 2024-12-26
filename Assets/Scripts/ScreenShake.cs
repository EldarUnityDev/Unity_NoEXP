using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    Vector3 normalPosition;
    Vector3 desiredPosition;
    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;
    private void Awake()
    {
        References.screenShake = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
    }

    // Update is called once per frame
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
