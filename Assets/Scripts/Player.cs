using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject tracker;

    private CarController carController;
    public GameObject respawnPos;
    public GameObject[] targCubes = new GameObject[42];
    public int targCubeTrack;
    public Slider BoostBar;
    public int cubeNum;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        tracker.transform.position = targCubes[targCubeTrack].transform.position;
        tracker.transform.rotation = targCubes[targCubeTrack].transform.rotation;

        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool boost = Input.GetKey(KeyCode.LeftShift);
        BoostBar.value = carController.boost / carController.maxBoost;

        carController.GetInput(forwardInput, turnInput, jump, boost);
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tracker)
        {
            tracker.GetComponent<BoxCollider>().enabled = false;
            respawnPos.transform.position = other.transform.position;
            respawnPos.transform.rotation = other.transform.rotation;          
            targCubeTrack += 1;
            if (targCubeTrack == (cubeNum))
            {
                targCubeTrack = 0;
            }
            yield return new WaitForSeconds(0);
            tracker.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            transform.position = respawnPos.transform.position;
            transform.rotation = respawnPos.transform.rotation;
        }
    }
}
