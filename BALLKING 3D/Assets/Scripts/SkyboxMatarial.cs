using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMatarial : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = mat;
        gameObject.GetComponent<Skybox>().material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
