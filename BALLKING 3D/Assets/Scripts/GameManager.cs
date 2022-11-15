using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uimanager;
    public AdManager admanager;
   
   


    


    public void Start()
    {
         
        CoinCalculator(0);
        Debug.Log(PlayerPrefs.GetInt("moneyy"));
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("FinishLine"))
        {

            Debug.Log("Oyun bitti");
            admanager.RequestInterstitial();
            admanager.RequestRewardedAd();
            CoinCalculator(100);
            uimanager.CoinTextUpdate();
            uimanager.FinishScreen();
         


            PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
            

            StartCoroutine(Wait_For_Seconds(1));
        }
    }
    //        if (PlayerPrefs.GetInt("Noads") == 0)
    //        {

    //        }



       
    //}

    public void CoinCalculator(int money)
    {
        if (PlayerPrefs.HasKey("moneyy"))
        {
            int oldScore = PlayerPrefs.GetInt("moneyy");
            PlayerPrefs.SetInt("moneyy", oldScore + money);
        }
        else
            PlayerPrefs.SetInt("moneyy", 0);
    }

    IEnumerator Wait_For_Seconds(int sec)
    {
        float elapsed_time = Time.deltaTime;

        while (elapsed_time < sec)
        {

            yield return new WaitForSeconds(Time.deltaTime);
            elapsed_time += Time.deltaTime;
        }

        FindObjectOfType<Player>().forwardSpeed = 0;
        FindObjectOfType<CameraMove>().forwardSpeed = 0;
    }
}
   


