using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject admob;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject direction;
    [SerializeField] GameObject evennt;
    [SerializeField] GameObject finish;
    [SerializeField] GameObject Ground;
    [SerializeField] GameObject ipmanager;
    [SerializeField] GameObject intro_hand;
    [SerializeField] GameObject maincam;
    [SerializeField] GameObject player;
    [SerializeField] GameObject snow;
    [SerializeField] GameObject sound;
    [SerializeField] GameObject text;
    [SerializeField] GameObject touch;
    [SerializeField] GameObject vectorback;
    [SerializeField] GameObject vectorforward;
    [SerializeField] GameObject vector;
    [SerializeField] GameObject Skybox;

    




    // Start is called before the first frame update
    void Start()
    {

        Level();
        obje();


    }
    // Update is called once per frame
    void Update()
    {

    }
    void Level()
    {

        for (int i = 0; i < 1; i++)
        {

            Instantiate(admob);
            Instantiate(cam);
            Instantiate(canvas);
            Instantiate(direction);
            Instantiate(evennt);
            Instantiate(finish);
            Instantiate(Ground);
            Instantiate(ipmanager);
            Instantiate(intro_hand);
            //Instantiate(maincam);
            Instantiate(player);
            Instantiate(snow);
            Instantiate(sound);
            Instantiate(text);
            Instantiate(touch);
            Instantiate(vectorback);
            Instantiate(vectorforward);
            Instantiate(vector);
            Instantiate(Skybox);

        }




    }

    void obje()
    {
        
       
    }
}


