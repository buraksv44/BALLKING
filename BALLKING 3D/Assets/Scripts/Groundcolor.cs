using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Groundcolor : MonoBehaviour
{
    [SerializeField] MeshRenderer ground;
    // Start is called before the first frame update
    void Start()
    {
        ground.material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
