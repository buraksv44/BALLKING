using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Transform vectorback;
    public Transform vectorforward;
    public Transform vectorleft;
    public Transform vectorright;


    public void LateUpdate()
    {
        Vector3 viewPOs = transform.position;
        viewPOs.z = Mathf.Clamp(viewPOs.z,vectorback.transform.position.z,vectorforward.transform.position.z);
        viewPOs.x = Mathf.Clamp(viewPOs.x, vectorleft.transform.position.x, vectorright.transform.position.x);
        transform.position = viewPOs;
    }

}
