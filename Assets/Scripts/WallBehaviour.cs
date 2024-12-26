using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float speed;
    public float speedUpFraction;
    public float speedDownFraction;
    public Rigidbody ourRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ourRigidBody.velocity = direction * speed;

    }
}
