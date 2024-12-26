using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        References.navPoints.Add(this);
    }
    // Update is called once per frame
    private void OnDisable()
    {
        References.navPoints.Remove(this);

    }
    private void Update()
    {
        
    }
}
