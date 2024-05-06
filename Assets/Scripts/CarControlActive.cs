using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlActive : MonoBehaviour
{
    public GameObject playerCarControl;
    public GameObject AICarControl1;
    public GameObject AICarControl2;
    public GameObject stream1;
    public GameObject stream2;
    public GameObject stream3;
    public GameObject wheels1;
    public GameObject wheels2;
    public GameObject wheels3;
    public BoxCollider bx1;
    public BoxCollider bx2;
    public BoxCollider bx3;
    void Start()
    {
        bx1.GetComponent<BoxCollider>().enabled = false;
        bx2.GetComponent<BoxCollider>().enabled = false;
        bx3.GetComponent<BoxCollider>().enabled = false;
        wheels1.SetActive(true);
        wheels2.SetActive(true);
        wheels3.SetActive(true);
        playerCarControl.GetComponent<CarController>().enabled = true;
        AICarControl1.GetComponent<CarController>().enabled = true;
        AICarControl2.GetComponent<CarController>().enabled = true;
        stream1.SetActive(true);
        stream2.SetActive(true);
        stream3.SetActive(true);
    }
}
