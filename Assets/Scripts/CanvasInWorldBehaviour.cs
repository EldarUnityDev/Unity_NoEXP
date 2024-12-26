using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInWorldBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() //we need Awake to establish the reference before IT IS USED
    {
        References.canvasInWorld = this;
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
