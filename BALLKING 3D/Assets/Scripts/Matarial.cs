using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matarial : MonoBehaviour
{
    [SerializeField] MeshRenderer  mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh.material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
