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

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
        //store our position relative to the player
        cameraOffset = transform.position - References.thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //set our position by looking at the players position+adding offset
        if(References.thePlayer != null)
        {
            normalPosition = References.thePlayer.transform.position + cameraOffset;
        }

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
