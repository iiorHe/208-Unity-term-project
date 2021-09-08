using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardY : MonoBehaviour
{
    public Transform _lookPos;
    void Start()
    {
        _lookPos = PlayerBase.instance.transform;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = _lookPos.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(_lookPos.position - v); 
    }
}
