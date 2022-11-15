using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Light>().color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
