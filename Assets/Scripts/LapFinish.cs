using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapFinish : MonoBehaviour
{
    public GameObject finishTrig;
    public GameObject CheckPointTrig;

    public GameObject minDisplay;
    public GameObject SecDisplay;
    public GameObject milDisplay;

    public GameObject lapDisplay;
    public GameObject finText;
    public GameObject placeDisplay;
    public Button menu;

    public GameObject pc;
    public GameObject cai;

    private int lapCount = 1;
    private int place = 0;

    public int minNum = 500;
    public int secNum = 500;
    public int milNum = 500;

    private void OnTriggerEnter(Collider other)
    {
        if(lapCount == 3)
        {
            place++;
            if(other.CompareTag("PlayerCar"))
            {
                StartCoroutine(ToMenu());
                finText.GetComponent<TMP_Text>().text = "FINISH";
                finText.SetActive(true);
                if(place == 1)
                {
                    placeDisplay.GetComponent<TMP_Text>().text = "1st Place";
                }
                if (place == 2)
                {
                    placeDisplay.GetComponent<TMP_Text>().text = "2nd Place";
                }
                if (place == 3)
                {
                    placeDisplay.GetComponent<TMP_Text>().text = "3rd Place";
                }
                pc.GetComponent<Player>().enabled = false;
                cai.GetComponent<CarAI>().enabled = true;
                cai.GetComponent<CarAI>().targCubeTrack = pc.GetComponent<Player>().targCubeTrack;             
            }
        }
        if(other.CompareTag("PlayerCar"))
        {
            if (lapCount != 3)
            {
                lapCount++;
            }           
            lapDisplay.GetComponent<TMP_Text>().text = "" + lapCount + "/3"; 
            if (StopWatch.minCount < minNum)
            {
                minNum = StopWatch.minCount;
                secNum = StopWatch.secCount;
                milNum = StopWatch.milCount;
                if (StopWatch.secCount <= 9)
                {
                    SecDisplay.GetComponent<TMP_Text>().text = "0" + secNum + ":";
                }
                else
                {
                    SecDisplay.GetComponent<TMP_Text>().text = "" + secNum + ":";
                }

                if (StopWatch.minCount <= 9)
                {
                    minDisplay.GetComponent<TMP_Text>().text = "0" + minNum + ":";
                }
                else
                {
                    minDisplay.GetComponent<TMP_Text>().text = "" + minNum + ":";
                }

                if (StopWatch.milCount <= 9)
                {
                    milDisplay.GetComponent<TMP_Text>().text = "0" + milNum;
                }
                else
                {
                    milDisplay.GetComponent<TMP_Text>().text = "" + milNum;
                }
            }
            else if (StopWatch.secCount < secNum)
            {
                secNum = StopWatch.secCount;
                milNum = StopWatch.milCount;
                if (StopWatch.secCount <= 9)
                {
                    SecDisplay.GetComponent<TMP_Text>().text = "0" + secNum + ":";
                }
                else
                {
                    SecDisplay.GetComponent<TMP_Text>().text = "" + secNum + ":";
                }
                if (StopWatch.milCount <= 9)
                {
                    milDisplay.GetComponent<TMP_Text>().text = "0" + milNum;
                }
                else
                {
                    milDisplay.GetComponent<TMP_Text>().text = "" + milNum;
                }
            }
            else if (StopWatch.milCount < milNum)
            {
                milNum = StopWatch.milCount;
                if (StopWatch.milCount <= 9)
                {
                    milDisplay.GetComponent<TMP_Text>().text = "0" + milNum;
                }
                else
                {
                    milDisplay.GetComponent<TMP_Text>().text = "" + milNum;
                }
            }
            StopWatch.minCount = 0;
            StopWatch.secCount = 0;
            StopWatch.milCalc = 0;
            if (lapCount != 3)
            {
                CheckPointTrig.SetActive(true);
                finishTrig.SetActive(false);
            }
        }        

        IEnumerator ToMenu()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }
    }
}
