using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ımage : MonoBehaviour
{
    [SerializeField] Image square;
    [SerializeField] Image fillRate;
    [SerializeField] Image leftBallBg;
    [SerializeField] Image rightBallBg;
    [SerializeField] Image leftFrongImg;
    [SerializeField] Image rightFrongImg;
   



    // Start is called before the first frame update
    void Start()
    {
        game();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void game ()
    {
        square.GetComponent<Image>().color = Random.ColorHSV();
        fillRate.GetComponent<Image>().color = Random.ColorHSV();
        leftBallBg.GetComponent<Image>().color = Random.ColorHSV();
        rightBallBg.GetComponent<Image>().color = Random.ColorHSV();
        leftFrongImg.GetComponent<Image>().color = Random.ColorHSV();
        rightBallBg.GetComponent<Image>().color = Random.ColorHSV();
       
    }
}
