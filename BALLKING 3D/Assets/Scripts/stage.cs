using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using TMPro.Examples;

public class stage : MonoBehaviour
{


    public TextMesh textMesh;
    public TextMesh mesh;

    // Start is called before the first frame update
    void Start()
    {


        Stagetext();




    }


    // Update is called once per frame
    void Update()
    {
       
    }

    void Stagetext()
    {
        textMesh.GetComponent<TextMesh>().color = Random.ColorHSV();
        mesh.GetComponent<TextMesh>().color = Random.ColorHSV();
        //PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        mesh.text = CurrentLevel.ToString();
        mesh.text = (CurrentLevel + 1).ToString();

    }

}
