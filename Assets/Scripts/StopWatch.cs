using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    public static int minCount;
    public static int secCount;
    public static int milCount;
    public static float milCalc;

    public GameObject minBox;
    public GameObject secBox;
    public GameObject milBox;
    void Update()
    {
        milCalc += Time.deltaTime * 100;
        milCount = Convert.ToInt32(milCalc);
        milBox.GetComponent<TMP_Text>().text = "" + milCount;

        if(milCount > 99)
        {
            milBox.GetComponent<TMP_Text>().text = "00";
        }

        if (milCount <= 9)
        {
            milBox.GetComponent<TMP_Text>().text = "0" + milCount;
        }

        if (milCount >= 100)
        {
            milCalc = 0;
            secCount += 1;
        }

        if (secCount <= 9)
        {
            secBox.GetComponent<TMP_Text>().text = "0" + secCount + ":";
        }
        else
        {
            secBox.GetComponent<TMP_Text>().text = "" + secCount + ":";
        }
        if(secCount >= 60)
        {
            secCount = 0;
            minCount += 1;
        }

        if(minCount <= 9)
        {
            minBox.GetComponent<TMP_Text>().text = "0" + minCount + ":";
        }
        else
        {
            minBox.GetComponent<TMP_Text>().text = "" + minCount + ":";
        }
    }
}
