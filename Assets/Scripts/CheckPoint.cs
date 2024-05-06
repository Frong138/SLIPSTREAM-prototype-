using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject finishTrigger;
    public GameObject checkPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            finishTrigger.SetActive(true);
            checkPoint.SetActive(false);
        }      
    }
}
